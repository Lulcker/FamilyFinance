using FamilyFinance.Application.Contracts.Providers;
using FamilyFinance.Persistence;
using FamilyFinance.Domain.Entities;
using FamilyFinance.DTO.Categories.RequestModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyFinance.Application.Commands.Categories;

/// <summary>
/// Команда обновления категории
/// </summary>
public class UpdateCategoryCommand(
    IRepository<Category> categoryRepository,
    IUnitOfWork unitOfWork,
    IUserInfoProvider userInfoProvider,
    ILogger<UpdateCategoryCommand> logger
    )
{
    public async Task ExecuteAsync(UpdateCategoryRequestModel requestModel, CancellationToken cancellationToken)
    {
        var newCategoryName = requestModel.NewName.Trim().UppercaseFirstLetter();

        newCategoryName.IsNotEmpty()
            .ThrowIfInvalidCondition("Название категории не указано");
        
        var category = await categoryRepository
            .SingleOrDefaultAsync(c => c.Id == requestModel.CategoryId, cancellationToken);
        
        category.ThrowIfNull("Категория не найдена");
        
        var existCategory = await categoryRepository
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.Name == newCategoryName && c.Id != category.Id, cancellationToken);
        
        existCategory.ThrowIfNotNull("Категория с таким названием уже существует");
        
        category.Name = newCategoryName;
        category.MonthlyPlan = requestModel.MonthlyPlan;
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Обновлена категория пользователем {UserName} (Email: {UserEmail})",
            userInfoProvider.Name, userInfoProvider.Email);
    }
}