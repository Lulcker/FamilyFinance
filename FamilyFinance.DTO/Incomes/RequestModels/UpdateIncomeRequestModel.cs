using FamilyFinance.DTO.Dictionaries;

namespace FamilyFinance.DTO.Incomes.RequestModels;

/// <summary>
/// Входная модель для обновления дохода
/// </summary>
public class UpdateIncomeRequestModel
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
    /// Комментарий
    /// </summary>
    public required string? Comment { get; init; }
}