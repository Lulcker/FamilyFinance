using FamilyFinance.DTO.Categories.ResponseModels;
using FamilyFinance.DTO.Expenses.RequestModels;
using FamilyFinance.ProxyApiMethods.ApiMethods;
using FamilyFinance.UI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FamilyFinance.UI.Dialogs.Expenses;

public partial class AddExpensesDialog(
    CategoriesApiHelper categoriesApiHelper,
    ExpensesApiHelper expensesApiHelper,
    ISnackbarHelper snackbarHelper
    ) : ComponentBase
{
    #region Parameters

    [Parameter, EditorRequired]
    public required EventCallback AfterSave { get; set; }

    #endregion
    
    #region Fields

    private List<CategoryResponseModel> categories = [];

    private List<AddExpenseRequestModel> newExpenses = [];

    private bool isOpened;

    private bool isLoading;
    
    private List<DateGroup> dateGroups = [];
    
    private bool creatingDate;

    private DateTime? creatingDateDateTime;

    #endregion

    #region Dialog Options

    private static readonly DialogOptions Options = new()
    {
        FullWidth = true,
        MaxWidth = MaxWidth.Small
    };

    #endregion

    #region Properties

    private bool IsSaveButtonDisabled => newExpenses.Count < 1;
                                         

    #endregion
    
    #region Methods

    private void BeginAddDate()
    {
        creatingDate = true;
    }

    private void CancelAddDate()
    {
        creatingDate = false;
        creatingDateDateTime = null;
    }

    private void ConfirmAddDate()
    {
        if (creatingDateDateTime is null)
            return;

        var date = DateOnly.FromDateTime(creatingDateDateTime.Value.Date);
        
        if (date > DateOnly.FromDateTime(DateTime.Now.Date) || dateGroups.Any(x => x.Date == date))
        {
            creatingDate = false;
            creatingDateDateTime = null;
            return;
        }

        dateGroups.Add(new DateGroup { Date = date });
        creatingDate = false;
        creatingDateDateTime = null;
    }
    
    private static void AddDraft(DateGroup group) =>
        group.Drafts.Add(new AddExpenseRequestModel
        {
            Date = default,
            Amount = 0,
            IsPersonal = false,
            CategoryId = Guid.Empty,
            Comment = null
        });

    private static void RemoveDraft(DateGroup group, AddExpenseRequestModel draft) =>
        group.Drafts.Remove(draft);
    
    private void SaveExpense(AddExpenseRequestModel model, DateGroup group, AddExpenseRequestModel draft)
    {
        newExpenses.Add(model);
        
        group.Drafts.Remove(draft);
        
        StateHasChanged();
    }
    
    private bool IsDateDisabledFunc(DateTime date) =>
        dateGroups.Any(x => x.Date == DateOnly.FromDateTime(date));

    private void RemoveExpense(AddExpenseRequestModel model)
    {
        newExpenses.Remove(model);
        StateHasChanged();
    }

    public async Task OpenAsync()
    {
        await LoadCategories();
        
        isOpened = true;
        StateHasChanged();
    }

    private async Task LoadCategories() =>
        categories = [.. await categoriesApiHelper.AllAsync()];
    
    private async Task SaveAsync()
    {
        if (IsSaveButtonDisabled)
            return;
        
        isLoading = true;

        try
        {
            await expensesApiHelper.BulkAddAsync(newExpenses);

            await snackbarHelper.ShowSuccess("Расходы добавлены");

            await Close(executeAfterSave: true);
        }
        finally
        {
            isLoading = false;
            await InvokeAsync(StateHasChanged);
        }
    }

    private async Task Close(bool executeAfterSave = false)
    {
        isOpened = false;

        categories = [];
        creatingDate = false;
        creatingDateDateTime = null;
        dateGroups = [];
        newExpenses = [];

        if (executeAfterSave)
            await AfterSave.InvokeAsync();
    }

    private string GetCategoryName(Guid categoryId) =>
        categories.First(c => c.Id == categoryId).Name;

    #endregion
    
    private class DateGroup
    {
        public DateOnly Date { get; init; }
        
        public List<AddExpenseRequestModel> Drafts { get; } = [];
    }
}