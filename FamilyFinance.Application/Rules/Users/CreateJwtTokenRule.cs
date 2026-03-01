using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FamilyFinance.Application.Contracts.Providers;
using FamilyFinance.Application.Contracts.Services;
using FamilyFinance.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace FamilyFinance.Application.Rules.Users;

/// <summary>
/// Правило создания JWT токена для пользователя
/// </summary>
public class CreateJwtTokenRule(
    IAuthProvider authProvider,
    IAesCryptoService aesCryptoService
    )
{
    internal string CreateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.Sid, user.Id.ToString()),
            new (ClaimTypes.Name, user.Name),
            new (ClaimTypes.Email, aesCryptoService.Decrypt(user.Email))
        };

        var now = DateTime.UtcNow;

        var jwt = new JwtSecurityToken(
            issuer: authProvider.Issuer,
            audience: authProvider.Audience,
            notBefore: now,
            claims: claims,
            expires: now.AddDays(60),
            signingCredentials: new SigningCredentials(authProvider.GetSecurityKey(), 
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}