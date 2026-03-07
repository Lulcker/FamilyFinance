using FamilyFinance.DTO.Categories.ResponseModels;
using FamilyFinance.DTO.Reports.RequestModels;
using FamilyFinance.DTO.Reports.ResponseModels;
using FamilyFinance.ProxyApiMethods.ApiMethods;
using FamilyFinance.UI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FamilyFinance.UI.Pages.Reports;

public partial class ReportPage(
    CategoriesApiHelper categoriesApiHelper,
    ReportsApiHelper reportsApiHelper,
    IBreadcrumbHelper breadcrumbHelper,
    NavigationManager navigationManager
    ) : ComponentBase
{
    #region Fields
    
    private bool isLoading;

    private List<ExpensesByCategoryResponseModel> expensesByCategories = [];

    private List<CategoryResponseModel> categories = [];

    private List<CategoryResponseModel> excludeCategories = [];

    #endregion

    #region Methods

    protected override async Task OnInitializedAsync()
    {
        breadcrumbHelper.SetBreadcrumbs(
        [
            new BreadcrumbItem("Отчёт", "/")
        ]);

        await LoadCategories();
        
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        isLoading = true;

        expensesByCategories = [.. await reportsApiHelper.ExpensesByCategoriesAsync(new ExpensesByCategoryRequestModel
        {
            ExcludeCategoryIds = [.. excludeCategories.Select(c => c.Id)]
        })];

        isLoading = false;
    }

    private async Task LoadCategories() =>
        categories = [.. await categoriesApiHelper.AllAsync()];

    private async Task ExcludeCategoryIdsChanged(IEnumerable<CategoryResponseModel>? categoryResponseModels)
    {
        excludeCategories = categoryResponseModels is not null ? categoryResponseModels.ToList() : [];

        await LoadDataAsync();
    }
    
    private void GoToExpenses() =>
        navigationManager.NavigateTo("/expenses");
    
    private void GoToIncomes() =>
        navigationManager.NavigateTo("/incomes");
    
    private static string BoldTextStyle(ExpensesByCategoryResponseModel expensesByCategory) =>
        expensesByCategory.Name is "Итого" ? "font-weight: bold" : string.Empty;

    #endregion

    #region Funfs

    private static Func<ExpensesByCategoryResponseModel, string> NameStyleFunc => BoldTextStyle;

    private static Func<ExpensesByCategoryResponseModel, string> AverageStyleFunc => expensesByCategory =>
        expensesByCategory.Average > expensesByCategory.MonthlyPlan
            ? "background-color: #f4c7c3"
            : string.Empty;
    
    private static Func<ExpensesByCategoryResponseModel, string> JanuaryStyleFunc => expensesByCategory =>
        expensesByCategory.ExpensesInJanuary > expensesByCategory.MonthlyPlan
            ? "background-color: #f4c7c3"
            : string.Empty;
    
    private static Func<ExpensesByCategoryResponseModel, string> FebruaryStyleFunc => expensesByCategory =>
        expensesByCategory.ExpensesInFebruary > expensesByCategory.MonthlyPlan
            ? "background-color: #f4c7c3"
            : string.Empty;
    
    private static Func<ExpensesByCategoryResponseModel, string> MarchStyleFunc => expensesByCategory =>
        expensesByCategory.ExpensesInMarch > expensesByCategory.MonthlyPlan
            ? "background-color: #f4c7c3"
            : string.Empty;
    
    private static Func<ExpensesByCategoryResponseModel, string> AprilStyleFunc => expensesByCategory =>
        expensesByCategory.ExpensesInApril > expensesByCategory.MonthlyPlan
            ? "background-color: #f4c7c3"
            : string.Empty;
    
    private static Func<ExpensesByCategoryResponseModel, string> MayStyleFunc => expensesByCategory =>
        expensesByCategory.ExpensesInMay > expensesByCategory.MonthlyPlan
            ? "background-color: #f4c7c3"
            : string.Empty;
    
    private static Func<ExpensesByCategoryResponseModel, string> JuneStyleFunc => expensesByCategory =>
        expensesByCategory.ExpensesInJune > expensesByCategory.MonthlyPlan
            ? "background-color: #f4c7c3"
            : string.Empty;
    
    private static Func<ExpensesByCategoryResponseModel, string> JulyStyleFunc => expensesByCategory =>
        expensesByCategory.ExpensesInJuly > expensesByCategory.MonthlyPlan
            ? "background-color: #f4c7c3"
            : string.Empty;
    
    private static Func<ExpensesByCategoryResponseModel, string> AugustStyleFunc => expensesByCategory =>
        expensesByCategory.ExpensesInAugust > expensesByCategory.MonthlyPlan
            ? "background-color: #f4c7c3"
            : string.Empty;
    
    private static Func<ExpensesByCategoryResponseModel, string> SeptemberStyleFunc => expensesByCategory =>
        expensesByCategory.ExpensesInSeptember > expensesByCategory.MonthlyPlan
            ? "background-color: #f4c7c3"
            : string.Empty;
    
    private static Func<ExpensesByCategoryResponseModel, string> OctoberStyleFunc => expensesByCategory =>
        expensesByCategory.ExpensesInOctober > expensesByCategory.MonthlyPlan
            ? "background-color: #f4c7c3"
            : string.Empty;
    
    private static Func<ExpensesByCategoryResponseModel, string> NovemberStyleFunc => expensesByCategory =>
        expensesByCategory.ExpensesInNovember > expensesByCategory.MonthlyPlan
            ? "background-color: #f4c7c3"
            : string.Empty;
    
    private static Func<ExpensesByCategoryResponseModel, string> DecemberStyleFunc => expensesByCategory =>
        expensesByCategory.ExpensesInDecember > expensesByCategory.MonthlyPlan
            ? "background-color: #f4c7c3"
            : string.Empty;

    #endregion
}