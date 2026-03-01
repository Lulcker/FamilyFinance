using FamilyFinance.UI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FamilyFinance.UI.Pages.Reports;

public partial class ReportPage(
    IBreadcrumbHelper breadcrumbHelper,
    NavigationManager navigationManager
    ) : ComponentBase
{
    protected override Task OnInitializedAsync()
    {
        breadcrumbHelper.SetBreadcrumbs(
        [
            new BreadcrumbItem("Отчёт", "/")
        ]);
        
        return Task.CompletedTask;
    }
    
    private void GoToExpenses() =>
        navigationManager.NavigateTo("/expenses");
    
    private void GoToIncomes() =>
        navigationManager.NavigateTo("/incomes");
}