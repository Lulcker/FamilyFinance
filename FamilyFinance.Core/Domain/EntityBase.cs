namespace FamilyFinance.Core.Domain;

/// <summary>
/// Базовая сущность
/// </summary>
public class EntityBase
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; } = Guid.CreateVersion7();
}