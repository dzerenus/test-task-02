using Bank.Core.Enums;

namespace Bank.App.Models;

/// <summary>
/// Результат конвертирования валюты.
/// </summary>
/// <param name="currency">Валюта, в которую свершилось конвертирование.</param>
/// <param name="amount">Сумма, полученная после конвертирования.</param>
public class CurrencyAmount(
    Currency currency, 
    decimal amount)
{
    /// <summary>
    /// Валюта, в которую были конвертированы средства.
    /// </summary>
    public Currency Currency { get; } = currency;

    /// <summary>
    /// Сумма-результат конвертации средств.
    /// </summary>
    public decimal Amount { get; } = amount;
}
