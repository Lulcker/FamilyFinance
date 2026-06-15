using FamilyFinance.DTO.Categories.RequestModels;
using FamilyFinance.DTO.Categories.ResponseModels;
using FamilyFinance.ProxyApiMethods.ApiMethods;
using FamilyFinance.UI.Contracts;
using FamilyFinance.UI.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace FamilyFinance.UI.Pages.Categories;

public partial class CategoriesPage(
    CategoriesApiHelper categoriesApiHelper,
    IBreadcrumbHelper breadcrumbHelper,
    ISnackbarHelper snackbarHelper
    ) : ComponentBase
{
    #region Fields

    private List<CategoryResponseModel> categories = [];

    private bool isDataLoading;
    
    private bool isAddedMode;
    
    private bool isAddLoading;
    
    private string newCategoryName = string.Empty;

    private double newCategoryMonthlyPlan;
    
    private HashSet<string> auxiliaryWords = [];

    private string auxiliaryWord = string.Empty;

    #endregion
    
    #region Properties

    private bool IsSaveButtonDisabled => newCategoryName.IsEmpty() || isAddLoading;

    #endregion

    #region Methods

    protected override async Task OnInitializedAsync()
    {
        breadcrumbHelper.SetBreadcrumbs(
        [
            new BreadcrumbItem("На главную", "/")
        ]);
        
        await LoadDataAsync();
    }
    
    private async Task LoadDataAsync()
    {
        isDataLoading = true;

        categories = [.. await categoriesApiHelper.AllAsync()];
        
        isDataLoading = false;
    }
    
    private void SetAddedMode() => isAddedMode = true;

    private void UnsetAndClearAddedMode()
    {
        newCategoryName = string.Empty;
        newCategoryMonthlyPlan = 0;
        auxiliaryWords = [];
        auxiliaryWord = string.Empty;
        
        isAddedMode = false;
    }

    private void AddAuxiliaryWord()
    {
        if (auxiliaryWord.IsEmpty())
            return;
        
        auxiliaryWords.Add(auxiliaryWord.Trim());
        auxiliaryWord = string.Empty;
    }

    private void RemoveAuxiliaryWord(string word)
    {
        auxiliaryWords.Remove(word);
    }

    private async Task SaveAsync()
    {
        if (IsSaveButtonDisabled)
            return;
        
        isAddLoading = true;

        try
        {
            await categoriesApiHelper.AddAsync(new AddCategoryRequestModel
            {
                Name = newCategoryName.Trim(),
                MonthlyPlan = newCategoryMonthlyPlan,
                AuxiliaryWords = auxiliaryWords
            });
            
            UnsetAndClearAddedMode();

            await snackbarHelper.ShowSuccess("Категория добавлена");

            await LoadDataAsync();
        }
        finally
        {
            isAddLoading = false;
            await InvokeAsync(StateHasChanged);
        }
    }
    
    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        switch (e.Key)
        {
            case "Enter":
                await SaveAsync();
                break;
            case "Escape":
                UnsetAndClearAddedMode();
                break;
        }
    }
    
    private void HandleKeyDownForAuxiliaryWord(KeyboardEventArgs e)
    {
        switch (e.Key)
        {
            case "Enter":
                AddAuxiliaryWord();
                break;
        }
    }

    #endregion
}