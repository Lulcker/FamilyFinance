using FamilyFinance.Application.Contracts.Providers;
using FamilyFinance.Persistence;
using FamilyFinance.Domain.Entities;
using FamilyFinance.DTO.Expenses.RequestModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyFinance.Application.Commands.Expenses;

/// <summary>
/// Команда добавления расходов
/// </summary>
public class AddExpensesCommand(
    IRepository<Expense> expenseRepository,
    IRepository<Category> categoryRepository,
    IUnitOfWork unitOfWork,
    IUserInfoProvider userInfoProvider,
    ILogger<AddExpensesCommand> logger
    )
{
    public async Task ExecuteAsync(IReadOnlyCollection<AddExpenseRequestModel> newExpenses, CancellationToken cancellationToken)
    {
        foreach (var newExpense in newExpenses)
        {
            (newExpense.Date <= DateOnly.FromDateTime(DateTime.UtcNow))
                .ThrowIfInvalidCondition("Дата расхода должна быть меньше или равна текущей дате");
        
            (newExpense.Amount > 0)
                .ThrowIfInvalidCondition("Сумма расхода должна быть больше 0");
            
            var category = await categoryRepository
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.Id == newExpense.CategoryId, cancellationToken);

            category
                .ThrowIfNull($"Категория для расхода с датой {newExpense.Date} и суммой {newExpense.Amount} не найдена. Попробуйте перезагрузить страницу");
            
            expenseRepository.Add(new Expense
            {
                Date = newExpense.Date,
                Amount = newExpense.Amount,
                IsPersonal = newExpense.IsPersonal,
                UserId = userInfoProvider.Id,
                CategoryId = newExpense.CategoryId,
                Comment = newExpense.Comment
            });
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Добавлено расходов {NewExpensesCount} шт. пользователем {UserName} (Email: {UserEmail})", 
            newExpenses.Count, userInfoProvider.Name, userInfoProvider.Email);
    }
}