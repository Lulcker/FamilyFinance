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

        var query = categoryRepository
            .AsNoTracking();

        if (requestModel.ExcludeCategoryIds.Count != 0)
            query = query.Where(c => !requestModel.ExcludeCategoryIds.Contains(c.Id));
        
        var result = await query
            .Select(c => new ExpensesByCategoryResponseModel
            {
                Name = c.Name,
                MonthlyPlan = c.MonthlyPlan,
                Average = (c.Expenses
                    .Where(e => !e.IsPersonal && e.Date.Year == dateTimeUtcNow.Year && dateTimeUtcNow.Month >= e.Date.Month)
                    .Select(e => (double?)e.Amount)
                    .Sum() ?? 0) / dateTimeUtcNow.Month,
                ExpensesInJanuary = dateTimeUtcNow.Month >= 1 
                    ? c.Expenses
                        .Where(e => !e.IsPersonal && e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 1)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInFebruary = dateTimeUtcNow.Month >= 2
                    ? c.Expenses
                        .Where(e => !e.IsPersonal && e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 2)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInMarch = dateTimeUtcNow.Month >= 3
                    ? c.Expenses
                        .Where(e => !e.IsPersonal && e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 3)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInApril = dateTimeUtcNow.Month >= 4 
                    ? c.Expenses
                        .Where(e => !e.IsPersonal && e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 4)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInMay = dateTimeUtcNow.Month >= 5
                    ? c.Expenses
                        .Where(e => !e.IsPersonal && e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 5)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInJune = dateTimeUtcNow.Month >= 6
                    ? c.Expenses
                        .Where(e => !e.IsPersonal && e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 6)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInJuly = dateTimeUtcNow.Month >= 7
                    ? c.Expenses
                        .Where(e => !e.IsPersonal && e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 7)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInAugust = dateTimeUtcNow.Month >= 8
                    ? c.Expenses
                        .Where(e => !e.IsPersonal && e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 8)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInSeptember = dateTimeUtcNow.Month >= 9
                    ? c.Expenses
                        .Where(e => !e.IsPersonal && e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 9)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInOctober = dateTimeUtcNow.Month >= 10
                    ? c.Expenses
                        .Where(e => !e.IsPersonal && e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 10)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInNovember = dateTimeUtcNow.Month >= 11
                    ? c.Expenses
                        .Where(e => !e.IsPersonal && e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 11)
                        .Sum(e => e.Amount)
                    : null,
                ExpensesInDecember = dateTimeUtcNow.Month >= 12
                    ? c.Expenses
                        .Where(e => !e.IsPersonal && e.Date.Year == dateTimeUtcNow.Year && e.Date.Month == 12)
                        .Sum(e => e.Amount)
                    : null
            })
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);
        
        result.Add(new ExpensesByCategoryResponseModel
        {
            Name = "Итого",
            MonthlyPlan = result.Sum(r => r.MonthlyPlan),
            Average = result.Sum(r => r.Average),
            ExpensesInJanuary = result.Any(r => r.ExpensesInJanuary.HasValue) 
                ? result.Sum(r => r.ExpensesInJanuary) 
                : null,
            ExpensesInFebruary = result.Any(r => r.ExpensesInFebruary.HasValue) 
                ? result.Sum(r => r.ExpensesInFebruary) 
                : null,
            ExpensesInMarch = result.Any(r => r.ExpensesInMarch.HasValue) 
                ? result.Sum(r => r.ExpensesInMarch) 
                : null,
            ExpensesInApril = result.Any(r => r.ExpensesInApril.HasValue) 
                ? result.Sum(r => r.ExpensesInApril) 
                : null,
            ExpensesInMay = result.Any(r => r.ExpensesInMay.HasValue) 
                ? result.Sum(r => r.ExpensesInMay) 
                : null,
            ExpensesInJune = result.Any(r => r.ExpensesInJune.HasValue) 
                ? result.Sum(r => r.ExpensesInJune) 
                : null,
            ExpensesInJuly = result.Any(r => r.ExpensesInJuly.HasValue) 
                ? result.Sum(r => r.ExpensesInJuly) 
                : null,
            ExpensesInAugust = result.Any(r => r.ExpensesInAugust.HasValue) 
                ? result.Sum(r => r.ExpensesInAugust) 
                : null,
            ExpensesInSeptember = result.Any(r => r.ExpensesInSeptember.HasValue) 
                ? result.Sum(r => r.ExpensesInSeptember) 
                : null,
            ExpensesInOctober = result.Any(r => r.ExpensesInOctober.HasValue) 
                ? result.Sum(r => r.ExpensesInOctober) 
                : null,
            ExpensesInNovember = result.Any(r => r.ExpensesInNovember.HasValue) 
                ? result.Sum(r => r.ExpensesInNovember) 
                : null,
            ExpensesInDecember = result.Any(r => r.ExpensesInDecember.HasValue) 
                ? result.Sum(r => r.ExpensesInDecember) 
                : null
        });

        return result;
    }
}