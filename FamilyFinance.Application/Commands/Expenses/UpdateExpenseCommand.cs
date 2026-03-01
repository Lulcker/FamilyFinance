using FamilyFinance.Application.Contracts.Providers;
using FamilyFinance.Persistence;
using FamilyFinance.Domain.Entities;
using FamilyFinance.DTO.Expenses.RequestModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyFinance.Application.Commands.Expenses;

/// <summary>
/// Команда обновления расхода
/// </summary>
public class UpdateExpenseCommand(
    IRepository<Expense> expenseRepository,
    IRepository<Category> categoryRepository,
    IUnitOfWork unitOfWork,
    IUserInfoProvider userInfoProvider,
    ILogger<UpdateExpenseCommand> logger
    )
{
    public async Task ExecuteAsync(UpdateExpenseRequestModel requestModel, CancellationToken cancellationToken)
    {
        (requestModel.Date <= DateOnly.FromDateTime(DateTime.UtcNow))
            .ThrowIfInvalidCondition("Дата расхода должна быть меньше или равна текущей дате");
        
        (requestModel.Amount > 0)
            .ThrowIfInvalidCondition("Сумма расхода должна быть больше 0");
        
        var expense = await expenseRepository
            .SingleOrDefaultAsync(e => e.Id == requestModel.Id, cancellationToken);
        
        expense.ThrowIfNull("Расход не найден");
        
        (expense.UserId == userInfoProvider.Id)
            .ThrowIfInvalidCondition("Нельзя редактировать чужой расход");
        
        var category = await categoryRepository
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.Id == requestModel.CategoryId, cancellationToken);

        category
            .ThrowIfNull("Категория не найдена. Попробуйте перезагрузить страницу");

        expense.Date = requestModel.Date;
        expense.Amount = requestModel.Amount;
        expense.IsPersonal = requestModel.IsPersonal;
        expense.CategoryId = requestModel.CategoryId;
        expense.Comment = requestModel.Comment;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Обновлён расход с Id: {ExpenseId} пользователем {UserName} (Email: {UserEmail})", 
            expense.Id, userInfoProvider.Name, userInfoProvider.Email);
    }
}