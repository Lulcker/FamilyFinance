using FamilyFinance.DTO.Categories.RequestModels;
using FamilyFinance.DTO.Categories.ResponseModels;
using FamilyFinance.ProxyApiMethods.ApiMethods;
using FamilyFinance.UI.Contracts;
using FamilyFinance.UI.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace FamilyFinance.UI.Components.Categories;

public partial class CategoryPaper(
    CategoriesApiHelper categoriesApiHelper,
    ISnackbarHelper snackbarHelper
    ) : ComponentBase
{
    #region Parameters

    /// <summary>
    /// Категория
    /// </summary>
    [Parameter, EditorRequired]
    public required CategoryResponseModel Category { get; set; }
    
    /// <summary>
    /// Действие сохранения
    /// </summary>
    [Parameter, EditorRequired]
    public required EventCallback OnSave { get; set; }

    #endregion

    #region Fields
    
    private bool isEditMode;

    private bool isUpdateLoading;
    
    private string newCategoryName = string.Empty;
    
    private double newCategoryMonthlyPlan;

    #endregion

    #region Properties

    private bool IsSaveButtonDisabled => (newCategoryName.Trim().Equals(Category.Name, StringComparison.CurrentCultureIgnoreCase) && newCategoryMonthlyPlan == Category.MonthlyPlan)
                                         || isUpdateLoading;

    #endregion

    #region Methods

    private void SetUpdateMode()
    {
        isEditMode = true;
        newCategoryName = Category.Name;
        newCategoryMonthlyPlan = Category.MonthlyPlan;
    }

    private void UnsetAndClearUpdateMode()
    {
        isEditMode = false;
        
        newCategoryName = string.Empty;
        newCategoryMonthlyPlan = 0;
    }

    private async Task SaveAsync()
    {
        if (IsSaveButtonDisabled)
            return;
        
        isUpdateLoading = true;

        try
        {
            await categoriesApiHelper.UpdateAsync(new UpdateCategoryRequestModel
            {
                CategoryId = Category.Id,
                NewName = newCategoryName.Trim(),
                MonthlyPlan = newCategoryMonthlyPlan
            });
            
            await snackbarHelper.ShowSuccess("Категория обновлена");

            Category.Name = newCategoryName.Trim().UppercaseFirstLetter();
            Category.MonthlyPlan = newCategoryMonthlyPlan;

            UnsetAndClearUpdateMode();
        }
        finally
        {
            isUpdateLoading = false;
            await InvokeAsync(StateHasChanged);
        }
    }

    private async Task DeleteAsync()
    {
        if (!await snackbarHelper.ShowConfirm("Вы уверены, что хотите удалить категорию?"))
            return;
        
        await categoriesApiHelper.DeleteAsync(Category.Id);

        await snackbarHelper.ShowSuccess("Категория удалена");

        await OnSave.InvokeAsync();
    }
    
    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        switch (e.Key)
        {
            case "Enter":
                await SaveAsync();
                break;
            case "Escape":
                UnsetAndClearUpdateMode();
                break;
        }
    }

    #endregion
}