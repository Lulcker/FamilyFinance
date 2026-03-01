using FamilyFinance.Application.Commands.Categories;
using FamilyFinance.Application.Queries.Categories;
using FamilyFinance.DTO.Categories.RequestModels;
using FamilyFinance.DTO.Categories.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFinance.Controllers;

/// <summary>
/// Контроллер категорий
/// </summary>
[Authorize]
[ApiController]
[Route("api/category")]
public class CategoriesController(
    GetAllCategoriesQuery getAllCategoriesQuery,
    AddCategoryCommand addCategoryCommand,
    UpdateCategoryCommand updateCategoryCommand,
    DeleteCategoryCommand deleteCategoryCommand
    ) : ControllerBase
{
    #region GET

    /// <summary>
    /// Получение списка категорий
    /// </summary>
    /// <returns>Список категорий</returns>
    [HttpGet("all")]
    public async Task<ActionResult<IReadOnlyCollection<CategoryResponseModel>>> AllAsync(CancellationToken cancellationToken) =>
        Ok(await getAllCategoriesQuery.ExecuteAsync(cancellationToken));

    #endregion

    #region POST

    /// <summary>
    /// Создание категории
    /// </summary>
    /// <param name="requestModel">Входная модель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("add")]
    public async Task<IActionResult> AddAsync([FromBody] AddCategoryRequestModel requestModel, CancellationToken cancellationToken)
    {
        await addCategoryCommand.ExecuteAsync(requestModel, cancellationToken);
        return Ok();
    }

    #endregion

    #region PATCH

    /// <summary>
    /// Обновление категории
    /// </summary>
    /// <param name="requestModel">Входная модель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPatch("update")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateCategoryRequestModel requestModel, CancellationToken cancellationToken)
    {
        await updateCategoryCommand.ExecuteAsync(requestModel, cancellationToken);
        return Ok();
    }

    #endregion

    #region DELETE

    /// <summary>
    /// Удаление категории
    /// </summary>
    /// <param name="categoryId">Id категории</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete("delete/{categoryId:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid categoryId, CancellationToken cancellationToken)
    {
        await deleteCategoryCommand.ExecuteAsync(categoryId, cancellationToken);
        return Ok();
    }

    #endregion
}