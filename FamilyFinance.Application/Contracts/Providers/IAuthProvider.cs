using Microsoft.IdentityModel.Tokens;

namespace FamilyFinance.Application.Contracts.Providers;

/// <summary>
/// Провайдер авторизации
/// </summary>
public interface IAuthProvider
{
    string Key { get; }
    
    string Issuer { get; }
    
    string Audience { get; }

    SymmetricSecurityKey GetSecurityKey();
}