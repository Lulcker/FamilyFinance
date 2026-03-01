using FamilyFinance.Application.Contracts.Providers;
using FamilyFinance.Persistence;
using FamilyFinance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyFinance.Application.Commands.Categories;

/// <summary>
/// Команда удаления категории
/// </summary>
public class DeleteCategoryCommand(
    IRepository<Category> categoryRepository,
    IUnitOfWork unitOfWork,
    IUserInfoProvider userInfoProvider,
    ILogger<DeleteCategoryCommand> logger
    )
{
    public async Task ExecuteAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var category = await categoryRepository
            .Include(c => c.Expenses)
            .SingleOrDefaultAsync(c => c.Id == categoryId, cancellationToken);
        
        category.ThrowIfNull("Категория не найдена");
        
        (category.Expenses.Count is 0)
            .ThrowIfInvalidCondition("Нельзя удалить категорию, которая связана с тратами");
        
        categoryRepository.Remove(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Удалена категория {CategoryName} пользователем {UserName} (Email: {UserEmail})",
            category.Name, userInfoProvider.Name, userInfoProvider.Email);
    }
}