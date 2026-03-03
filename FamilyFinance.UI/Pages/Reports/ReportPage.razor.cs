using FamilyFinance.DTO.Reports.RequestModels;
using FamilyFinance.DTO.Reports.ResponseModels;
using FamilyFinance.ProxyApiMethods.ApiMethods;
using FamilyFinance.UI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FamilyFinance.UI.Pages.Reports;

public partial class ReportPage(
    ReportsApiHelper reportsApiHelper,
    IBreadcrumbHelper breadcrumbHelper,
    NavigationManager navigationManager
    ) : ComponentBase
{
    #region Fields
    
    private bool isLoading;

    private List<ExpensesByCategoryResponseModel> expensesByCategories = [];

    #endregion

    #region Methods

    protected override async Task OnInitializedAsync()
    {
        breadcrumbHelper.SetBreadcrumbs(
        [
            new BreadcrumbItem("Отчёт", "/")
        ]);

        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        isLoading = true;

        expensesByCategories = [.. await reportsApiHelper.ExpensesByCategoriesAsync(new ExpensesByCategoryRequestModel
        {
            ExcludeCategoryIds = []
        })];

        isLoading = false;
    }
    
    private void GoToExpenses() =>
        navigationManager.NavigateTo("/expenses");
    
    private void GoToIncomes() =>
        navigationManager.NavigateTo("/incomes");

    private static Func<ExpensesByCategoryResponseModel, string> CellStyleFunc => expensesByCategory =>
        expensesByCategory.Average > expensesByCategory.MonthlyPlan
            ? "background-color: #f4c7c3"
            : string.Empty;

    #endregion
}