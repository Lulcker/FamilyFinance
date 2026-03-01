using FamilyFinance.Application.Contracts.Providers;
using FamilyFinance.Application.Contracts.Services;
using FamilyFinance.Domain.Entities;
using FamilyFinance.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyFinance.Application.Commands.Inits;

public class CreateDefaultUsersCommand(
    IRepository<User> userRepository,
    IUnitOfWork unitOfWork,
    IHashService hashService,
    IAesCryptoService aesCryptoService,
    IDefaultUsersProvider defaultUsersProvider,
    ILogger<CreateDefaultUsersCommand> logger
    )
{
    public async Task ExecuteAsync()
    {
        if (await userRepository.AnyAsync())
            return;
        
        foreach (var defaultUser in defaultUsersProvider.DefaultUsers)
        {
            var salt = hashService.GenerateSalt();
            
            userRepository.Add(new User
            {
                Name = defaultUser.Name,
                Email = aesCryptoService.Encrypt(defaultUser.Email.ToLower()),
                PasswordHash = hashService.GenerateHash(defaultUser.Password, salt),
                PasswordSalt = salt,
                HexTransactionColor = defaultUser.HexTransactionColor
            });
            
            logger.LogInformation("Создан пользователь {UserName} (Email: {UserEmail})",
                defaultUser.Name, defaultUser.Email);
        }

        await unitOfWork.SaveChangesAsync();
    }
}