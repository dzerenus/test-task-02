using Bank.App.Models;
using Bank.Core.Enums;

namespace Bank.App.Interfaces;

/// <summary>
/// Сервис конвертирования валют.
/// </summary>
public interface IServiceExchange
{
    /// <summary>
    /// Конвертировать в Рубли.
    /// </summary>
    /// <param name="currency">Валюта, из которой происходит конвертация.</param>
    /// <param name="amount">Изначальная сумма в конвертируемой валюте.</param>
    /// <returns>Результат конвертации в Рублях.</returns>
    public CurrencyAmount ToRub(
        Currency currency, 
        decimal amount);

    /// <summary>
    /// Конвертировать в Доллары..
    /// </summary>
    /// <param name="currency">Валюта, из которой происходит конвертация.</param>
    /// <param name="amount">Изначальная сумма в конвертируемой валюте.</param>
    /// <returns>Результат конвертации в Долларах.</returns>
    public CurrencyAmount ToUsd(
        Currency currency, 
        decimal amount);
}
