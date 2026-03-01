using FamilyFinance.DTO.Dictionaries;

namespace FamilyFinance.DTO.Incomes.ResponseModels;

/// <summary>
/// Модель дохода
/// </summary>
public class IncomeResponseModel
{
    /// <summary>
    /// Id дохода
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Дата дохода
    /// </summary>
    public required DateOnly Date { get; init; }
    
    /// <summary>
    /// Сумма дохода
    /// </summary>
    public required double Amount { get; init; }
    
    /// <summary>
    /// Тип дохода
    /// </summary>
    public required IncomeType Type { get; init; }
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public required Guid UserId { get; init; }

    /// <summary>
    /// HEX цвет транзакции
    /// </summary>
    public required string HexTransactionColor { get; init; }

    /// <summary>
    /// Комментарий
    /// </summary>
    public required string? Comment { get; init; }
}