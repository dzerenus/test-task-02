using Bank.Core.Enums;

namespace Bank.Core.Models;

/// <summary>
/// Кошешёк, относительно которого могут происходить транзакции.
/// </summary>
public class Wallet : BaseModel
{
    /// <summary>
    /// Название кошелька.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Валюта кошелька.
    /// </summary>
    public Currency Currency { get; }

    /// <summary>
    /// Стартовый баланс кошелька.
    /// </summary>
    public decimal InitialBalance { get; }

    /// <summary>
    /// Создание НОВОГО кошелька.
    /// </summary>
    /// <param name="title">Название кошелька.</param>
    /// <param name="currency">Валюта кошелька.</param>
    /// <param name="initialBalance">Стартовый баланс кошелька.</param>
    public Wallet(
        string title,
        Currency currency,
        decimal initialBalance)
    {
        Title = title;
        InitialBalance = initialBalance;
        Currency = currency;
    }

    /// <summary>
    /// Копирование кошелька.
    /// </summary>
    /// <param name="model">Копируемый кошелёк.</param>
    public Wallet(Wallet model) : base(model)
    {
        Title = model.Title;
        Currency = model.Currency;
        InitialBalance = model.InitialBalance;
    }
    
    /// <summary>
    /// Восстановление кошелька, например, из базы данных.
    /// </summary>
    /// <param name="id">ID кошелька.</param>
    /// <param name="title">Название кошелька.</param>
    /// <param name="currency">Валюта кошелька.</param>
    /// <param name="initialBalance">Стартовый баланс.</param>
    /// <param name="createdAtUtc">Дата создания кошелька.</param>
    /// <param name="updatedAtUtc">Дата последнего изменения кошелька.</param>
    public Wallet(
        Guid id,
        string title,
        Currency currency,
        decimal initialBalance,
        DateTime createdAtUtc,
        DateTime updatedAtUtc)
        : base(
            id: id,
            createdAtUtc: createdAtUtc,
            updatedAtUtc: updatedAtUtc)
    {
        Title = title;
        Currency = currency;
        InitialBalance = initialBalance;
    }
}
