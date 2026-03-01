using FamilyFinance.DTO.Dictionaries;
using FamilyFinance.DTO.Incomes.RequestModels;
using FamilyFinance.DTO.Incomes.ResponseModels;
using FamilyFinance.ProxyApiMethods.ApiMethods;
using FamilyFinance.UI.Contracts;
using FamilyFinance.UI.Dialogs.Incomes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace FamilyFinance.UI.Pages.Incomes;

public partial class IncomesPage(
    IncomesApiHelper incomesApiHelper,
    IBreadcrumbHelper breadcrumbHelper,
    ISnackbarHelper snackbarHelper,
    IUserSession userSession,
    NavigationManager navigationManager
    ) : ComponentBase
{
    #region Fields

    private List<IncomeResponseModel> incomes = [];

    private bool isDataLoading;
    
    private bool isAddedMode;
    
    private bool isLoading;

    private double newIncomeAmount;

    private IncomeType newIncomeType = IncomeType.Salary;

    private DateTime? newIncomeDateTime;

    private string? newIncomeComment;

    #endregion

    #region Refs

    private EditIncomeDialog editIncomeDialog = null!;

    #endregion
    
    #region Properties

    private bool IsSaveButtonDisabled => newIncomeAmount == 0 || !newIncomeDateTime.HasValue || isLoading;

    #endregion

    #region Methods

    protected override async Task OnInitializedAsync()
    {
        breadcrumbHelper.SetBreadcrumbs(
        [
            new BreadcrumbItem("Доходы", "/incomes")
        ]);
        
        await LoadDataAsync();
    }
    
    private async Task LoadDataAsync()
    {
        isDataLoading = true;

        incomes = [.. await incomesApiHelper.AllAsync()];
        
        isDataLoading = false;
    }
    
    private void SetAddedMode() => isAddedMode = true;

    private void UnsetAndClearAddedMode()
    {
        newIncomeAmount = 0;
        newIncomeDateTime = null;
        newIncomeType = IncomeType.Salary;
        newIncomeComment = null;
        
        isAddedMode = false;
    }

    private async Task SaveAsync()
    {
        if (IsSaveButtonDisabled)
            return;
        
        isLoading = true;

        try
        {
            await incomesApiHelper.AddAsync(new AddIncomeRequestModel
            {
                Date = DateOnly.FromDateTime(newIncomeDateTime!.Value),
                Amount = newIncomeAmount,
                Type = newIncomeType,
                Comment = newIncomeComment
            });
            
            UnsetAndClearAddedMode();

            await snackbarHelper.ShowSuccess("Доход добавлен");

            await LoadDataAsync();
        }
        finally
        {
            isLoading = false;
            await InvokeAsync(StateHasChanged);
        }
    }

    private void OpenEditDialog(IncomeResponseModel income) =>
        editIncomeDialog.Open(income);

    private async Task DeleteIncomeAsync(Guid incomeId)
    {
        if (!await snackbarHelper.ShowConfirm("Вы уверены, что хотите удалить доход?"))
            return;
        
        isLoading = true;

        try
        {
            await incomesApiHelper.DeleteAsync(incomeId);
            
            await snackbarHelper.ShowSuccess("Доход удалён");

            await LoadDataAsync();
        }
        finally
        {
            isDataLoading = false;
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

    private void GoToReport() =>
        navigationManager.NavigateTo("/report");
    
    private void GoToExpenses() =>
        navigationManager.NavigateTo("/expenses");
    
    private static string RowStyleFunc(IncomeResponseModel income, int _) =>
        $"background-color: {income.HexTransactionColor}";

    #endregion
}