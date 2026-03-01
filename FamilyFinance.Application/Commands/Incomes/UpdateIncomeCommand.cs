using FamilyFinance.Application.Contracts.Providers;
using FamilyFinance.Persistence;
using FamilyFinance.Domain.Entities;
using FamilyFinance.DTO.Incomes.RequestModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyFinance.Application.Commands.Incomes;

/// <summary>
/// Команда обновления дохода
/// </summary>
public class UpdateIncomeCommand(
    IRepository<Income> incomeRepository,
    IUnitOfWork unitOfWork,
    IUserInfoProvider userInfoProvider,
    ILogger<UpdateIncomeCommand> logger
    )
{
    public async Task ExecuteAsync(UpdateIncomeRequestModel requestModel, CancellationToken cancellationToken)
    {
        (requestModel.Date <= DateOnly.FromDateTime(DateTime.UtcNow))
            .ThrowIfInvalidCondition("Дата дохода должна быть меньше или равна текущей дате");
        
        (requestModel.Amount > 0)
            .ThrowIfInvalidCondition("Сумма дохода должна быть больше 0");
        
        var income = await incomeRepository
            .SingleOrDefaultAsync(i => i.Id == requestModel.Id, cancellationToken);
        
        income.ThrowIfNull("Доход не найден");
        
        (income.UserId == userInfoProvider.Id)
            .ThrowIfInvalidCondition("Нельзя редактировать чужой доход");

        income.Date = requestModel.Date;
        income.Amount = requestModel.Amount;
        income.Type = requestModel.Type;
        income.Comment = requestModel.Comment;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Обновлён доход с Id: {IncomeId} пользователем {UserName} (Email: {UserEmail})", 
            income.Id, userInfoProvider.Name, userInfoProvider.Email);
    }
}