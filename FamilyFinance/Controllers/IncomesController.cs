using FamilyFinance.Application.Commands.Incomes;
using FamilyFinance.Application.Queries.Incomes;
using FamilyFinance.DTO.Incomes.RequestModels;
using FamilyFinance.DTO.Incomes.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFinance.Controllers;

/// <summary>
/// Контроллер расходов
/// </summary>
[Authorize]
[ApiController]
[Route("api/income")]
public class IncomesController(
    GetAllIncomesQuery getAllIncomesQuery,
    AddIncomeCommand addIncomeCommand,
    UpdateIncomeCommand updateIncomeCommand,
    DeleteIncomeCommand deleteIncomeCommand
    ) : ControllerBase
{
    #region GET

    /// <summary>
    /// Получение списка доходов
    /// </summary>
    /// <returns>Список доходов</returns>
    [HttpGet("all")]
    public async Task<ActionResult<IReadOnlyCollection<IncomeResponseModel>>> AllAsync(CancellationToken cancellationToken) =>
        Ok(await getAllIncomesQuery.ExecuteAsync(cancellationToken));

    #endregion

    #region POST

    /// <summary>
    /// Создание дохода
    /// </summary>
    /// <param name="requestModel">Входная модель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("add")]
    public async Task<IActionResult> AddAsync([FromBody] AddIncomeRequestModel requestModel, CancellationToken cancellationToken)
    {
        await addIncomeCommand.ExecuteAsync(requestModel, cancellationToken);
        return Ok();
    }

    #endregion

    #region PATCH

    /// <summary>
    /// Обновление дохода
    /// </summary>
    /// <param name="requestModel">Входная модель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPatch("update")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateIncomeRequestModel requestModel, CancellationToken cancellationToken)
    {
        await updateIncomeCommand.ExecuteAsync(requestModel, cancellationToken);
        return Ok();
    }

    #endregion

    #region DELETE

    /// <summary>
    /// Удаление дохода
    /// </summary>
    /// <param name="incomeId">Id дохода</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete("delete/{incomeId:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid incomeId, CancellationToken cancellationToken)
    {
        await deleteIncomeCommand.ExecuteAsync(incomeId, cancellationToken);
        return Ok();
    }

    #endregion
}