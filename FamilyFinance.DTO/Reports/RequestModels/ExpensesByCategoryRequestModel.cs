namespace FamilyFinance.DTO.Reports.RequestModels;

public class ExpensesByCategoryRequestModel
{
    /// <summary>
    /// Исключенные категории
    /// </summary>
    public required IReadOnlyCollection<Guid> ExcludeCategoryIds { get; init; }
}