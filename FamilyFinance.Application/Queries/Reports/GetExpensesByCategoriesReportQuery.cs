using FamilyFinance.Domain.Entities;
using FamilyFinance.DTO.Reports.RequestModels;
using FamilyFinance.DTO.Reports.ResponseModels;
using FamilyFinance.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Application.Queries.Reports;

public class GetExpensesByCategoriesReportQuery(IRepository<Category> categoryRepository)
{
    public async Task<IReadOnlyCollection<ExpensesByCategoryResponseModel>> ExecuteAsync(ExpensesByCategoryRequestModel requestModel, CancellationToken cancellationToken)
    {
        var dateTimeUtcNow = DateTime.UtcNow;
        
        return await categoryRepository
            .AsNoTracking()
            .Select(c => new ExpensesByCategoryResponseModel
            {
                Id = c.Id,
                Name = c.Name,
                MonthlyPlan = c.MonthlyPlan,
                Average = c.Expenses
                    .Where(e => e.Date.Year == dateTimeUtcNow.Year && dateTimeUtcNow.Month >= e.Date.Month)
                    .Select(e => (double?)e.Amount)
                    .Average() ?? 0,
                ExpensesInJanuary = dateTimeUtcNow.Month >= 1 
                    ? c.Expenses
                        .Where(e => e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 1)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInFebruary = dateTimeUtcNow.Month >= 2
                    ? c.Expenses
                        .Where(e => e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 2)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInMarch = dateTimeUtcNow.Month >= 3
                    ? c.Expenses
                        .Where(e => e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 3)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInApril = dateTimeUtcNow.Month >= 4 
                    ? c.Expenses
                        .Where(e => e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 4)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInMay = dateTimeUtcNow.Month >= 5
                    ? c.Expenses
                        .Where(e => e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 5)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInJune = dateTimeUtcNow.Month >= 6
                    ? c.Expenses
                        .Where(e => e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 6)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInJuly = dateTimeUtcNow.Month >= 7
                    ? c.Expenses
                        .Where(e => e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 7)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInAugust = dateTimeUtcNow.Month >= 8
                    ? c.Expenses
                        .Where(e => e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 8)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInSeptember = dateTimeUtcNow.Month >= 9
                    ? c.Expenses
                        .Where(e => e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 9)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInOctober = dateTimeUtcNow.Month >= 10
                    ? c.Expenses
                        .Where(e => e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 10)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInNovember = dateTimeUtcNow.Month >= 11
                    ? c.Expenses
                        .Where(e => e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 11)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInDecember = dateTimeUtcNow.Month >= 12
                    ? c.Expenses
                        .Where(e => e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 12)
                        .Sum(e => e.Amount)
                    : null
            })
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);
    }
}