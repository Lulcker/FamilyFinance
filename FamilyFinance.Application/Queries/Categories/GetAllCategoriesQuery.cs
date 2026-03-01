using FamilyFinance.Persistence;
using FamilyFinance.Domain.Entities;
using FamilyFinance.DTO.Categories.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Application.Queries.Categories;

/// <summary>
/// Запрос получения всех категорий
/// </summary>
public class GetAllCategoriesQuery(IRepository<Category> categoryRepository)
{
    public async Task<IReadOnlyCollection<CategoryResponseModel>> ExecuteAsync(CancellationToken cancellationToken)
    {
        return await categoryRepository
            .AsNoTracking()
            .Select(c => new CategoryResponseModel
            {
                Id = c.Id,
                Name = c.Name,
                MonthlyPlan = c.MonthlyPlan
            })
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);
    }
}