using FamilyFinance.Application.Contracts.Providers;
using FamilyFinance.Persistence;
using FamilyFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyFinance.Application.Commands.Expenses;

/// <summary>
/// Команда удаления расхода
/// </summary>
public class DeleteExpenseCommand(
    IRepository<Expense> expenseRepository,
    IUnitOfWork unitOfWork,
    IUserInfoProvider userInfoProvider,
    ILogger<DeleteExpenseCommand> logger
    )
{
    public async Task ExecuteAsync(Guid expenseId, CancellationToken cancellationToken)
    {
        var expense = await expenseRepository
            .SingleOrDefaultAsync(e => e.Id == expenseId, cancellationToken);
        
        expense.ThrowIfNull("Расход не найден");
        
        (expense.UserId == userInfoProvider.Id)
            .ThrowIfInvalidCondition("Нельзя удалять чужой расход");
        
        expenseRepository.Remove(expense);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Удалён расход с Id: {ExpenseId} пользователем {UserName} (Email: {UserEmail})", 
            expense.Id, userInfoProvider.Name, userInfoProvider.Email);
    }
}