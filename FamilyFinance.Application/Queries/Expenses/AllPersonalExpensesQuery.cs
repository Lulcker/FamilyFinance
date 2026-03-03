using FamilyFinance.Application.Contracts.Providers;
using FamilyFinance.Domain.Entities;
using FamilyFinance.DTO.Expenses.ResponseModels;
using FamilyFinance.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Application.Queries.Expenses;

/// <summary>
/// Получение личных расходов за текущий год
/// </summary>
public class AllPersonalExpensesQuery(
    IRepository<Expense> expenseRepository,
    IUserInfoProvider userInfoProvider
    )
{
    public async Task<IReadOnlyCollection<ExpenseResponseModel>> ExecuteAsync(CancellationToken cancellationToken)
    {
        return await expenseRepository
            .AsNoTracking()
            .Where(x => x.Date.Year == DateTime.UtcNow.Year)
            .Where(e => e.IsPersonal && e.UserId == userInfoProvider.Id)
            .Select(e => new ExpenseResponseModel
            {
                Id = e.Id,
                Date = e.Date,
                Amount = e.Amount,
                UserId = e.UserId,
                CategoryId = e.CategoryId,
                CategoryName = e.Category.Name,
                HexTransactionColor = e.User.HexTransactionColor,
                Comment = e.Comment
            })
            .OrderByDescending(e => e.Date)
            .ToListAsync(cancellationToken);
    }
}