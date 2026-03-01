using FamilyFinance.Application.Contracts.Providers;
using FamilyFinance.Persistence;
using FamilyFinance.Domain.Entities;
using FamilyFinance.DTO.Categories.RequestModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyFinance.Application.Commands.Categories;

/// <summary>
/// Команда добавления категории
/// </summary>
public class AddCategoryCommand(
    IRepository<Category> categoryRepository,
    IUnitOfWork unitOfWork,
    IUserInfoProvider userInfoProvider,
    ILogger<AddCategoryCommand> logger
    )
{
    public async Task ExecuteAsync(AddCategoryRequestModel requestModel, CancellationToken cancellationToken)
    {
        var categoryName = requestModel.Name.Trim().UppercaseFirstLetter();

        categoryName.IsNotEmpty()
            .ThrowIfInvalidCondition("Название категории не указано");

        var category = await categoryRepository
            .SingleOrDefaultAsync(c => c.Name == categoryName, cancellationToken);
        
        category.ThrowIfNotNull("Категория с таким названием уже существует");

        category = new Category
        {
            Name = categoryName,
            MonthlyPlan = requestModel.MonthlyPlan
        };
        
        categoryRepository.Add(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Добавлена категория {CategoryName} пользователем {UserName} (Email: {UserEmail})",
            categoryName, userInfoProvider.Name, userInfoProvider.Email);
    }
}