namespace FamilyFinance.DTO.Expenses.RequestModels;

/// <summary>
/// Входная модель для обновления расхода
/// </summary>
public class UpdateExpenseRequestModel
{
    /// <summary>
    /// Id расхода
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Дата расхода
    /// </summary>
    public required DateOnly Date { get; init; }
    
    /// <summary>
    /// Сумма расхода
    /// </summary>
    public required double Amount { get; init; }
    
    /// <summary>
    /// Личный расход
    /// </summary>
    public required bool IsPersonal { get; init; }
    
    /// <summary>
    /// Id категории
    /// </summary>
    public required Guid CategoryId { get; init; }
    
    /// <summary>
    /// Комментарий
    /// </summary>
    public required string? Comment { get; init; }
}