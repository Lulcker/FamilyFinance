namespace FamilyFinance.DTO.Reports.ResponseModels;

/// <summary>
/// Траты по категории
/// </summary>
public class ExpensesByCategoryResponseModel
{
    /// <summary>
    /// Id категории
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Название категории
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Месячный план
    /// </summary>
    public required double MonthlyPlan { get; init; }
    
    /// <summary>
    /// Среднее значение
    /// </summary>
    public required double Average { get; init; }
    
    /// <summary>
    /// Траты за январь
    /// </summary>
    public required double? ExpensesInJanuary { get; init; }
    
    /// <summary>
    /// Траты за февраль
    /// </summary>
    public required double? ExpensesInFebruary { get; init; }
    
    /// <summary>
    /// Траты за март
    /// </summary>
    public required double? ExpensesInMarch { get; init; }
    
    /// <summary>
    /// Траты за апрель
    /// </summary>
    public required double? ExpensesInApril { get; init; }
    
    /// <summary>
    /// Траты за май
    /// </summary>
    public required double? ExpensesInMay { get; init; }
    
    /// <summary>
    /// Траты за июнь
    /// </summary>
    public required double? ExpensesInJune { get; init; }
    
    /// <summary>
    /// Траты за июль
    /// </summary>
    public required double? ExpensesInJuly { get; init; }
    
    /// <summary>
    /// Траты за август
    /// </summary>
    public required double? ExpensesInAugust { get; init; }
    
    /// <summary>
    /// Траты за сентябрь
    /// </summary>
    public required double? ExpensesInSeptember { get; init; }
    
    /// <summary>
    /// Траты за октябрь
    /// </summary>
    public required double? ExpensesInOctober { get; init; }
    
    /// <summary>
    /// Траты за ноябрь
    /// </summary>
    public required double? ExpensesInNovember { get; init; }
    
    /// <summary>
    /// Траты за декабрь
    /// </summary>
    public required double? ExpensesInDecember { get; init; }
}