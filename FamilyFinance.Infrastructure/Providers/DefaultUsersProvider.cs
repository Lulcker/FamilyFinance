using FamilyFinance.Application.Contracts.Providers;

namespace FamilyFinance.Infrastructure.Providers;

public class DefaultUsersProvider : IDefaultUsersProvider
{
    /// <summary>
    /// Дефолтные пользователи
    /// </summary>
    public required IReadOnlyCollection<DefaultUser> DefaultUsers { get; init; }
}