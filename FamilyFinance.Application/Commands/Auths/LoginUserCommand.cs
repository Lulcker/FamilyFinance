using FamilyFinance.Application.Contracts.Services;
using FamilyFinance.Application.Rules.Users;
using FamilyFinance.Domain.Entities;
using FamilyFinance.DTO.Auths.RequestModels;
using FamilyFinance.DTO.Auths.ResponseModels;
using FamilyFinance.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyFinance.Application.Commands.Auths;

/// <summary>
/// Команда входа в систему для пользователя
/// </summary>
public class LoginUserCommand(
    IRepository<User> userRepository,
    IHashService hashService,
    IAesCryptoService aesCryptoService,
    CreateJwtTokenRule createJwtTokenRule,
    ILogger<LoginUserCommand> logger
    )
{
    public async Task<AuthorizeUserResponseModel> ExecuteAsync(LoginUserRequestModel requestModel, CancellationToken cancellationToken)
    {
        var email = requestModel.Email.Trim().ToLower();
        
        email.ThrowIfEmpty("Почта не может быть пустой");
        requestModel.Password.ThrowIfEmpty("Пароль не может быть пустым");
        
        (requestModel.Password.Length >= 8)
            .ThrowIfInvalidCondition("Пароль не может быть меньше 8 символов");
        
        var user = await userRepository
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == aesCryptoService.Encrypt(email), cancellationToken);
        
        user.ThrowIfNull("Пользователь с такой почтой не найден");
        
        user = await userRepository
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == aesCryptoService.Encrypt(email) &&
                                      u.PasswordHash == hashService.GenerateHash(requestModel.Password, user.PasswordSalt), cancellationToken);
        
        user.ThrowIfNull("Неверный пароль");
        
        logger.LogInformation("Пользователь {UserFirstName} Email: {UserEmail} (Id: {UserId}) вошёл в систему", 
            user.Name, aesCryptoService.Decrypt(user.Email), user.Id);

        return new AuthorizeUserResponseModel
        {
            Token = createJwtTokenRule.CreateJwtToken(user),
            UserId = user.Id,
            Name = user.Name,
            Email = aesCryptoService.Decrypt(user.Email)
        };
    }
}