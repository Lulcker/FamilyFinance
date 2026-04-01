using FamilyFinance.DTO.Auths.ResponseModels;
using FamilyFinance.DTO.Expenses.RequestModels;

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
    /// Черновики расходов
    /// </summary>
    IReadOnlyCollection<AddExpenseRequestModel> ExpenseDrafts { get; }
    
    /// <summary>
    /// Список Id категорий, которые не нужно включать в список для отчёта
    /// </summary>
    IReadOnlyCollection<Guid> ExcludeCategoryIds { get; }

    /// <summary>
    /// Начать сессию модератора
    /// </summary>
    /// <param name="responseModel">Входная модель</param>
    Task StartSession(AuthorizeUserResponseModel responseModel);

    /// <summary>
    /// Завершить сессию
    /// </summary>
    Task EndSession();
    
    /// <summary>
    /// Записать список Id категорий, которые не нужно включать в список для отчёта
    /// </summary>
    /// <param name="excludeCategoryIds">Список Id категорий</param>
    Task SetExcludeCategoryIds(IReadOnlyCollection<Guid> excludeCategoryIds);

    /// <summary>
    /// Записать черновики расходов
    /// </summary>
    /// <param name="expenseDrafts">Список расходов</param>
    Task SetExpenseDrafts(IReadOnlyCollection<AddExpenseRequestModel> expenseDrafts);
}