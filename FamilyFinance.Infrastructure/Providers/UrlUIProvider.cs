using FamilyFinance.Application.Contracts.Providers;

namespace FamilyFinance.Infrastructure.Providers;

/// <summary>
/// Провайдер URL для UI
/// </summary>
public class UrlUIProvider : IUrlUIProvider
{
    /// <summary>
    /// URL для UI
    /// </summary>
    public required string Url { get; init; }
}