using FamilyFinance.DTO.Auths.ResponseModels;

namespace FamilyFinance.UI.Contracts;

public interface IUserSession
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    Guid UserId { get; }
    
    /// <summary>
    /// Токен доступа пользователя
    /// </summary>
    string Token { get; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Почта пользователя
    /// </summary>
    string Email { get; }

    /// <summary>
    /// Начать сессию модератора
    /// </summary>
    /// <param name="responseModel">Входная модель</param>
    Task StartSession(AuthorizeUserResponseModel responseModel);

    /// <summary>
    /// Завершить сессию
    /// </summary>
    Task EndSession();
}