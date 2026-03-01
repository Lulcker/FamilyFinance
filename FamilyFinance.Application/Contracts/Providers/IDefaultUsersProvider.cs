namespace FamilyFinance.Application.Contracts.Providers;

public interface IDefaultUsersProvider
{ 
    /// <summary>
    /// Дефолтные пользователи
    /// </summary>
    IReadOnlyCollection<DefaultUser> DefaultUsers { get; }
}

public record DefaultUser(string Name, string Email, string Password, string HexTransactionColor);