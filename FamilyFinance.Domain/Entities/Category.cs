using FamilyFinance.Core.Domain;

namespace FamilyFinance.Domain.Entities;

/// <summary>
/// Категория траты
/// </summary>
public class Category : EntityBase
{
    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Месячный план расходов
    /// </summary>
    public required double MonthlyPlan { get; set; }

    /// <summary>
    /// Траты
    /// </summary>
    public ICollection<Expense> Expenses { get; set; } = new HashSet<Expense>();
}