using FamilyFinance.Application.Contracts.Providers;
using FamilyFinance.Persistence;
using FamilyFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyFinance.Application.Commands.Incomes;

/// <summary>
/// Команда удаления дохода
/// </summary>
public class DeleteIncomeCommand(
    IRepository<Income> incomeRepository,
    IUnitOfWork unitOfWork,
    IUserInfoProvider userInfoProvider,
    ILogger<DeleteIncomeCommand> logger
    )
{
    public async Task ExecuteAsync(Guid incomeId, CancellationToken cancellationToken)
    {
        var income = await incomeRepository
            .SingleOrDefaultAsync(i => i.Id == incomeId, cancellationToken);
        
        income.ThrowIfNull("Доход не найден");
        
        (income.UserId == userInfoProvider.Id)
            .ThrowIfInvalidCondition("Нельзя удалять чужой доход");
        
        incomeRepository.Remove(income);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Удалён доход с Id: {ExpenseId} пользователем {UserName} (Email: {UserEmail})", 
            income.Id, userInfoProvider.Name, userInfoProvider.Email);
    }
}