using FamilyFinance.DTO.Dictionaries;
using FamilyFinance.DTO.Incomes.RequestModels;
using FamilyFinance.DTO.Incomes.ResponseModels;
using FamilyFinance.ProxyApiMethods.ApiMethods;
using FamilyFinance.UI.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace FamilyFinance.UI.Dialogs.Incomes;

public partial class EditIncomeDialog(
    IncomesApiHelper incomesApiHelper,
    ISnackbarHelper snackbarHelper
    ) : ComponentBase
{
    #region Parameters

    [Parameter, EditorRequired]
    public required EventCallback AfterSave { get; set; }

    #endregion
    
    #region Fields

    private IncomeResponseModel income = null!;

    private DateTime? newIncomeDateTime;

    private double newIncomeAmount;

    private IncomeType newIncomeType;

    private string? newIncomeComment;

    private bool isOpened;

    private bool isLoading;

    private static readonly DialogOptions Options = new()
    {
        FullWidth = true,
        MaxWidth = MaxWidth.Small
    };

    #endregion

    #region Properties

    private bool IsSaveButtonDisabled => DateOnly.FromDateTime(newIncomeDateTime!.Value)  == income.Date &&
                                         newIncomeAmount == income.Amount &&
                                         newIncomeType == income.Type &&
                                         newIncomeComment == income.Comment;
                                         

    #endregion
    
    #region Methods

    public void Open(IncomeResponseModel incomeResponseModel)
    {
        income = incomeResponseModel;

        newIncomeDateTime = income.Date.ToDateTime(TimeOnly.MinValue);
        newIncomeAmount = income.Amount;
        newIncomeType = income.Type;
        newIncomeComment = income.Comment;
        
        isOpened = true;
        StateHasChanged();
    }
    
    private async Task SaveAsync()
    {
        if (IsSaveButtonDisabled)
            return;
        
        isLoading = true;

        try
        {
            await incomesApiHelper.UpdateAsync(new UpdateIncomeRequestModel
            {
                Id = income.Id,
                Date = DateOnly.FromDateTime(newIncomeDateTime!.Value),
                Amount = newIncomeAmount,
                Type = newIncomeType,
                Comment = newIncomeComment
            });

            await snackbarHelper.ShowSuccess("Доход добавлен");

            await Close(true);
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

        if (executeAfterSave)
            await AfterSave.InvokeAsync();
    }
    
    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        switch (e.Key)
        {
            case "Enter":
                await SaveAsync();
                break;
        }
    }

    #endregion
}