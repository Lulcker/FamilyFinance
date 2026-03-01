using FamilyFinance.DTO.Categories.ResponseModels;
using FamilyFinance.DTO.Expenses.RequestModels;
using Microsoft.AspNetCore.Components;

namespace FamilyFinance.UI.Components.Expenses;

public partial class AddExpenseRow : ComponentBase
{
    [Parameter, EditorRequired] 
    public DateOnly Date { get; set; }
    
    [Parameter, EditorRequired] 
    public List<CategoryResponseModel> Categories { get; set; } = [];
    
    [Parameter] 
    public EventCallback<AddExpenseRequestModel> OnSave { get; set; }
    
    [Parameter] 
    public EventCallback OnRemove { get; set; }

    #region Fields

    private double amount;

    private bool isPersonal;
        
    private Guid? categoryId;

    private string? comment;

    #endregion

    private bool CanSave => amount > 0 && categoryId.HasValue;

    private async Task Save()
    {
        if (!CanSave)
            return;

        await OnSave.InvokeAsync(new AddExpenseRequestModel
        {
            Date = Date,
            Amount = amount,
            IsPersonal = isPersonal,
            CategoryId = categoryId!.Value,
            Comment = comment
        });
    }
}