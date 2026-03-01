using FamilyFinance.Application.Contracts.Providers;
using FamilyFinance.Persistence;
using FamilyFinance.Domain.Entities;
using FamilyFinance.DTO.Incomes.RequestModels;
using Microsoft.Extensions.Logging;

namespace FamilyFinance.Application.Commands.Incomes;

/// <summary>
/// Команда добавления дохода
/// </summary>
public class AddIncomeCommand(
    IRepository<Income> incomeRepository,
    IUnitOfWork unitOfWork,
    IUserInfoProvider userInfoProvider,
    ILogger<AddIncomeCommand> logger
    )
{
    public async Task ExecuteAsync(AddIncomeRequestModel requestModel, CancellationToken cancellationToken)
    {
        (requestModel.Date <= DateOnly.FromDateTime(DateTime.UtcNow))
            .ThrowIfInvalidCondition("Дата дохода должна быть меньше или равна текущей дате");
        
        (requestModel.Amount > 0)
            .ThrowIfInvalidCondition("Сумма дохода должна быть больше 0");
        
        var newIncome = new Income
        {
            Date = requestModel.Date,
            Amount = requestModel.Amount,
            Type = requestModel.Type,
            UserId = userInfoProvider.Id,
            Comment = requestModel.Comment
        };
        
        incomeRepository.Add(newIncome);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Добавлен доход с Id: {IncomeId} пользователем {UserName} (Email: {UserEmail})", 
            newIncome.Id, userInfoProvider.Name, userInfoProvider.Email);
    }
}