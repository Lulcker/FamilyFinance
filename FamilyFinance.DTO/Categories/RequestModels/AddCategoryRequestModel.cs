namespace FamilyFinance.DTO.Categories.RequestModels;

/// <summary>
/// Входная модель для добавления категории
/// </summary>
public class AddCategoryRequestModel
{
    /// <summary>
    /// Название категории
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Месячный план расходов
    /// </summary>
    public required double MonthlyPlan { get; set; }
}