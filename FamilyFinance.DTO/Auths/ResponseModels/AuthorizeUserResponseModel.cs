namespace FamilyFinance.DTO.Auths.ResponseModels;

/// <summary>
/// Модель с данными авторизации пользователя
/// </summary>
public class AuthorizeUserResponseModel
{
    /// <summary>
    /// Токен
    /// </summary>
    public required string Token { get; init; }
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public required Guid UserId { get; init; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Почта
    /// </summary>
    public required string Email { get; init; }
}