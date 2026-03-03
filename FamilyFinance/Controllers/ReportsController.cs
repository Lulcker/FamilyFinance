using FamilyFinance.Application.Queries.Reports;
using FamilyFinance.DTO.Reports.RequestModels;
using FamilyFinance.DTO.Reports.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFinance.Controllers;

/// <summary>
/// Контроллер авторизации
/// </summary>
[Authorize]
[ApiController]
[Route("api/report")]
public class ReportsController(GetExpensesByCategoriesReportQuery getExpensesByCategoriesReportQuery) : ControllerBase
{
    #region POST

    /// <summary>
    /// Получение списка расходов по категориям
    /// </summary>
    /// <returns>Список расходов по категориям</returns>
    [HttpPost("expenses-by-categories")]
    public async Task<ActionResult<IReadOnlyCollection<ExpensesByCategoryResponseModel>>> ExpensesByCategoriesAsync([FromBody] ExpensesByCategoryRequestModel requestModel, CancellationToken cancellationToken) =>
        Ok(await getExpensesByCategoriesReportQuery.ExecuteAsync(requestModel, cancellationToken));

    #endregion
}