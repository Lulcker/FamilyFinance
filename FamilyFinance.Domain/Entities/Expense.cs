using FamilyFinance.Core.Domain;

namespace FamilyFinance.Domain.Entities;

/// <summary>
/// Расход
/// </summary>
public class Expense : EntityBase
{
    /// <summary>
    /// Дата расхода
    /// </summary>
    public required DateOnly Date { get; set; }
    
    /// <summary>
    /// Сумма расхода
    /// </summary>
    public required double Amount { get; set; }
    
    /// <summary>
    /// Личный расход
    /// </summary>
    public required bool IsPersonal { get; set; }
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public required Guid UserId { get; init; }

    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; init; } = null!;
    
    /// <summary>
    /// Id категории
    /// </summary>
    public required Guid CategoryId { get; set; }

    /// <summary>
    /// Категория
    /// </summary>
    public Category Category { get; set; } = null!;

    /// <summary>
    /// Комментарий
    /// </summary>
    public required string? Comment { get; set; }
}