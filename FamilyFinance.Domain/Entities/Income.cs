using FamilyFinance.Core.Domain;
using FamilyFinance.DTO.Dictionaries;

namespace FamilyFinance.Domain.Entities;

/// <summary>
/// Доход
/// </summary>
public class Income : EntityBase
{
    /// <summary>
    /// Дата дохода
    /// </summary>
    public required DateOnly Date { get; set; }
    
    /// <summary>
    /// Сумма дохода
    /// </summary>
    public required double Amount { get; set; }
    
    /// <summary>
    /// Тип дохода
    /// </summary>
    public required IncomeType Type { get; set; }
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public required Guid UserId { get; init; }

    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; init; } = null!;
    
    /// <summary>
    /// Комментарий
    /// </summary>
    public required string? Comment { get; set; }
}