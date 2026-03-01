using FamilyFinance.Application.Commands.Auths;
using FamilyFinance.DTO.Auths.RequestModels;
using FamilyFinance.DTO.Auths.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace FamilyFinance.Controllers;

/// <summary>
/// Контроллер авторизации
/// </summary>
[ApiController]
[Route("api/auth")]
public class AuthsController(LoginUserCommand loginUserCommand) : ControllerBase
{
    #region POST

    /// <summary>
    /// Вход пользователя в систему
    /// </summary>
    /// <param name="requestModel">Входная модель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Данные авторизации</returns>
    [HttpPost("login")]
    public async Task<ActionResult<AuthorizeUserResponseModel>> LoginAsync([FromBody] LoginUserRequestModel requestModel, CancellationToken cancellationToken) =>
        Ok(await loginUserCommand.ExecuteAsync(requestModel, cancellationToken));

    #endregion
}