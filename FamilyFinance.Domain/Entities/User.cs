using FamilyFinance.Core.Domain;

namespace FamilyFinance.Domain.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class User : EntityBase
{
    /// <summary>
    /// Имя
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Почта
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// Хэш пароля
    /// </summary>
    public required string PasswordHash { get; set; }
    
    /// <summary>
    /// Соль пароля
    /// </summary>
    public required string PasswordSalt { get; set; }

    /// <summary>
    /// HEX цвет транзакции
    /// </summary>
    public required string HexTransactionColor { get; set; }

    /// <summary>
    /// Траты
    /// </summary>
    public ICollection<Expense> Expenses { get; set; } = new HashSet<Expense>();

    /// <summary>
    /// Доходы
    /// </summary>
    public ICollection<Income> Incomes { get; set; } = new HashSet<Income>();
}