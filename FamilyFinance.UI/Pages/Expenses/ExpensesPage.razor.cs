using FamilyFinance.DTO.Categories.ResponseModels;
using FamilyFinance.DTO.Expenses.ResponseModels;
using FamilyFinance.ProxyApiMethods.ApiMethods;
using FamilyFinance.UI.Contracts;
using FamilyFinance.UI.Dialogs.Expenses;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FamilyFinance.UI.Pages.Expenses;

public partial class ExpensesPage(
    ExpensesApiHelper expensesApiHelper,
    CategoriesApiHelper categoriesApiHelper,
    IBreadcrumbHelper breadcrumbHelper,
    ISnackbarHelper snackbarHelper,
    IUserSession userSession,
    NavigationManager navigationManager
    ) : ComponentBase
{
    #region Fields

    private List<ExpenseResponseModel> expenses = [];

    private List<CategoryResponseModel> categories = [];

    private bool isLoading;

    private bool isGeneralExpenses = true;

    #endregion

    #region Refs

    private AddExpensesDialog addExpensesDialog = null!;

    private EditExpenseDialog editExpenseDialog = null!;

    #endregion
    
    #region Methods

    protected override async Task OnInitializedAsync()
    {
        breadcrumbHelper.SetBreadcrumbs(
        [
            new BreadcrumbItem("Расходы", "/expenses")
        ]);
        
        await LoadCategories();
        
        await LoadDataAsync();
    }
    
    private async Task LoadDataAsync()
    {
        isLoading = true;

        if (isGeneralExpenses)
            expenses = [.. await expensesApiHelper.AllGeneralAsync()];
        else
            expenses = [..await expensesApiHelper.AllPersonalAsync()];
        
        isLoading = false;
    }
    
    private async Task LoadCategories() =>
        categories = [.. await categoriesApiHelper.AllAsync()];

    private async Task IsGeneralExpensesChange(bool value)
    {
        isGeneralExpenses = value;

        await LoadDataAsync();
    }

    private void OpenAddExpensesDialog() =>
        addExpensesDialog.Open();

    private async Task OpenEditExpenseDialogAsync(ExpenseResponseModel expense) =>
        await editExpenseDialog.OpenAsync(expense);
    
    private async Task DeleteExpenseAsync(Guid expenseId)
    {
        if (!await snackbarHelper.ShowConfirm("Вы уверены, что хотите удалить расход?"))
            return;
        
        isLoading = true;

        try
        {
            await expensesApiHelper.DeleteAsync(expenseId);
            
            await snackbarHelper.ShowSuccess("Расход удалён");

            await LoadDataAsync();
        }
        finally
        {
            isLoading = false;
            await InvokeAsync(StateHasChanged);
        }
    }
    
    private void GoToReport() =>
        navigationManager.NavigateTo("/report");
    
    private void GoToIncomes() =>
        navigationManager.NavigateTo("/incomes");
    
    private static string RowStyleFunc(ExpenseResponseModel expense, int _) =>
        $"background-color: {expense.HexTransactionColor}";

    #endregion
}