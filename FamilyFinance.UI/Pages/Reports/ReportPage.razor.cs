using FamilyFinance.UI.Contracts;
using Microsoft.AspNetCore.Components;

namespace FamilyFinance.UI.Pages.Reports;

public partial class ReportPage(
    IBreadcrumbHelper breadcrumbHelper,
    NavigationManager navigationManager
    ) : ComponentBase
{
    private void GoToExpenses() =>
        navigationManager.NavigateTo("/expenses");
    
    private void GoToIncomes() =>
        navigationManager.NavigateTo("/incomes");
}