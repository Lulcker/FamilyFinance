using FamilyFinance.Application.Commands.Expenses;
using FamilyFinance.Application.Queries.Expenses;
using FamilyFinance.DTO.Expenses.RequestModels;
using FamilyFinance.DTO.Expenses.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFinance.Controllers;

/// <summary>
/// Контроллер расходов
/// </summary>
[Authorize]
[ApiController]
[Route("api/expense")]
public class ExpensesController(
    AllGeneralExpensesQuery allGeneralExpensesQuery,
    AllPersonalExpensesQuery allPersonalExpensesQuery,
    AddExpensesCommand addExpensesCommand,
    UpdateExpenseCommand updateExpenseCommand,
    DeleteExpenseCommand deleteExpenseCommand
    ) : ControllerBase
{
    #region GET

    /// <summary>
    /// Получение списка общих расходов
    /// </summary>
    /// <returns>Список расходов</returns>
    [HttpGet("all-general")]
    public async Task<ActionResult<IReadOnlyCollection<ExpenseResponseModel>>> AllGeneralAsync(CancellationToken cancellationToken) =>
        Ok(await allGeneralExpensesQuery.ExecuteAsync(cancellationToken));
    
    /// <summary>
    /// Получение списка личных расходов
    /// </summary>
    /// <returns>Список расходов</returns>
    [HttpGet("all-personal")]
    public async Task<ActionResult<IReadOnlyCollection<ExpenseResponseModel>>> AllPersonalAsync(CancellationToken cancellationToken) =>
        Ok(await allPersonalExpensesQuery.ExecuteAsync(cancellationToken));

    #endregion

    #region POST

    /// <summary>
    /// Создание расходов
    /// </summary>
    /// <param name="requestModels">Входные данные</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("bulk-add")]
    public async Task<IActionResult> BulkAddAsync([FromBody] IReadOnlyCollection<AddExpenseRequestModel> requestModels, CancellationToken cancellationToken)
    {
        await addExpensesCommand.ExecuteAsync(requestModels, cancellationToken);
        return Ok();
    }

    #endregion

    #region PATCH

    /// <summary>
    /// Обновление расхода
    /// </summary>
    /// <param name="requestModel">Входная модель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPatch("update")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateExpenseRequestModel requestModel, CancellationToken cancellationToken)
    {
        await updateExpenseCommand.ExecuteAsync(requestModel, cancellationToken);
        return Ok();
    }

    #endregion

    #region DELETE

    /// <summary>
    /// Удаление расхода
    /// </summary>
    /// <param name="expenseId">Id расхода</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete("delete/{expenseId:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid expenseId, CancellationToken cancellationToken)
    {
        await deleteExpenseCommand.ExecuteAsync(expenseId, cancellationToken);
        return Ok();
    }

    #endregion
}