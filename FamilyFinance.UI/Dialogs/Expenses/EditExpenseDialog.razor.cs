using FamilyFinance.DTO.Categories.ResponseModels;
using FamilyFinance.DTO.Expenses.RequestModels;
using FamilyFinance.DTO.Expenses.ResponseModels;
using FamilyFinance.ProxyApiMethods.ApiMethods;
using FamilyFinance.UI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FamilyFinance.UI.Dialogs.Expenses;

public partial class EditExpenseDialog(
    ExpensesApiHelper expensesApiHelper,
    ISnackbarHelper snackbarHelper
    ) : ComponentBase
{
    #region Parameters
     
    [Parameter, EditorRequired] 
    public required List<CategoryResponseModel> Categories { get; set; }

    [Parameter, EditorRequired]
    public required EventCallback AfterSave { get; set; }

    #endregion
    
    #region Fields

    private ExpenseResponseModel expense = null!;
    
    private double? amount;

    private bool isPersonal;
        
    private Guid? categoryId;

    private string? comment;
    
    private DateTime? expenseDateTime;

    private bool isOpened;

    private bool isLoading;

    #endregion

    #region Dialog Options

    private static readonly DialogOptions Options = new()
    {
        FullWidth = true,
        MaxWidth = MaxWidth.Small
    };

    #endregion

    #region Properties

    private bool IsSaveButtonDisabled => !amount.HasValue ||
                                         amount <= 0 ||
                                         !expenseDateTime.HasValue ||
                                         (expense.Amount == amount && 
                                          expense.CategoryId == categoryId &&
                                          expense.Comment == comment &&
                                          expense.IsPersonal == isPersonal &&
                                          DateOnly.FromDateTime(expenseDateTime!.Value) == expense.Date);
                                         

    #endregion
    
    #region Methods

    public async Task OpenAsync(ExpenseResponseModel expenseResponseModel)
    {
        expense = expenseResponseModel;
        amount = expense.Amount;
        isPersonal = expense.IsPersonal;
        categoryId = expense.CategoryId;
        comment = expense.Comment;
        expenseDateTime = expense.Date.ToDateTime(TimeOnly.MinValue);
        
        isOpened = true;
        
        await Task.Yield();
        await InvokeAsync(StateHasChanged);
    }
    
    private async Task SaveAsync()
    {
        if (IsSaveButtonDisabled)
            return;
        
        isLoading = true;

        try
        {
            await expensesApiHelper.UpdateAsync(new UpdateExpenseRequestModel
            {
                Id = expense!.Id,
                Date = DateOnly.FromDateTime(expenseDateTime!.Value),
                Amount = amount!.Value,
                IsPersonal = isPersonal,
                CategoryId = categoryId!.Value,
                Comment = comment
            });

            await snackbarHelper.ShowSuccess("Расход обновлён");

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
        
        expense = null;
        comment = null;

        if (executeAfterSave)
            await AfterSave.InvokeAsync();
    }

    #endregion
}