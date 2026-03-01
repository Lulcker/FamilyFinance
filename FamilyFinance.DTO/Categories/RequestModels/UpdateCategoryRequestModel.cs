namespace FamilyFinance.DTO.Categories.RequestModels;

/// <summary>
/// Входная модель для добавления категории
/// </summary>
public class UpdateCategoryRequestModel
{
    /// <summary>
    /// Id категории
    /// </summary>
    public required Guid CategoryId { get; init; }
    
    /// <summary>
    /// Название категории
    /// </summary>
    public required string NewName { get; init; }
    
    /// <summary>
    /// Месячный план расходов
    /// </summary>
    public required double MonthlyPlan { get; set; }
}