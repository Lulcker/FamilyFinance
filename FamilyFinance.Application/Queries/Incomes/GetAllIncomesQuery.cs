using FamilyFinance.Persistence;
using FamilyFinance.Domain.Entities;
using FamilyFinance.DTO.Incomes.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Application.Queries.Incomes;

/// <summary>
/// Запрос получения всех доходов за текущий год
/// </summary>
public class GetAllIncomesQuery(IRepository<Income> incomeRepository)
{
    public async Task<IReadOnlyCollection<IncomeResponseModel>> ExecuteAsync(CancellationToken cancellationToken)
    {
        return await incomeRepository
            .AsNoTracking()
            .Where(x => x.Date.Year == DateTime.UtcNow.Year)
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