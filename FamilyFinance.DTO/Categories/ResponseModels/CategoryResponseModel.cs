namespace FamilyFinance.DTO.Categories.ResponseModels;

/// <summary>
/// Модель категории
/// </summary>
public class CategoryResponseModel
{
    /// <summary>
    /// Id категории
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Название категории
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Месячный план расходов
    /// </summary>
    public required double MonthlyPlan { get; set; }
}