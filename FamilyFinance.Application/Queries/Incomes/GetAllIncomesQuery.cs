using FamilyFinance.Persistence;
using FamilyFinance.Domain.Entities;
using FamilyFinance.DTO.Incomes.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Application.Queries.Incomes;

/// <summary>
/// Запрос получения всех доходов
/// </summary>
public class GetAllIncomesQuery(IRepository<Income> incomeRepository)
{
    public async Task<IReadOnlyCollection<IncomeResponseModel>> ExecuteAsync(CancellationToken cancellationToken)
    {
        return await incomeRepository
            .AsNoTracking()
            .Select(i => new IncomeResponseModel
            {
                Id = i.Id,
                Date = i.Date,
                Amount = i.Amount,
                Type = i.Type,
                UserId = i.UserId,
                HexTransactionColor = i.User.HexTransactionColor,
                Comment = i.Comment
            })
            .OrderByDescending(i => i.Date)
            .ToListAsync(cancellationToken);
    }
}