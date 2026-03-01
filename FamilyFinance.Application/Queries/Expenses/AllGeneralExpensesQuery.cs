using FamilyFinance.Persistence;
using FamilyFinance.Domain.Entities;
using FamilyFinance.DTO.Expenses.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Application.Queries.Expenses;

/// <summary>
/// Получение общих расходов
/// </summary>
public class AllGeneralExpensesQuery(IRepository<Expense> expenseRepository)
{
    public async Task<IReadOnlyCollection<ExpenseResponseModel>> ExecuteAsync(CancellationToken cancellationToken)
    {
        return await expenseRepository
            .AsNoTracking()
            .Where(e => !e.IsPersonal)
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