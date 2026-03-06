namespace FamilyFinance.DTO.Expenses.ResponseModels;

/// <summary>
/// Модель расхода
/// </summary>
public class ExpenseResponseModel
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
    /// Id пользователя
    /// </summary>
    public required Guid UserId { get; init; }
    
    /// <summary>
    /// Id категории
    /// </summary>
    public required Guid CategoryId { get; init; }

    /// <summary>
    /// Категория
    /// </summary>
    public required string CategoryName { get; init; }
    
    /// <summary>
    /// HEX цвет транзакции
    /// </summary>
    public required string HexTransactionColor { get; init; }

    /// <summary>
    /// Комментарий
    /// </summary>
    public required string? Comment { get; init; }
    
    /// <summary>
    /// Личная трата
    /// </summary>
    public required bool IsPersonal { get; init; }
}