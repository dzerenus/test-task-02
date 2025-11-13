using Bank.Core.Enums;
using Bank.App.Configuration;
using Bank.App.Interfaces;
using Bank.App.Models;

namespace Bank.App.Services;

/// <summary>
/// Сервис конвертирования валют.
/// </summary>
/// <param name="currencies">Конфигурация (курсы) валют.</param>
internal class ServiceExchange(IConfugurationCurrencies currencies) : IServiceExchange
{
    /// <summary>
    /// Конвертировать валюту в Рубли.
    /// </summary>
    /// <param name="currency">Конвертируемая валюта.</param>
    /// <param name="amount">Сумма конвертации.</param>
    /// <returns>Результат конвертации в виде суммы и валюты.</returns>
    public CurrencyAmount ToRub(
        Currency currency, 
        decimal amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);

        // Рубли в Рубли.
        if (currency == Currency.RUB)
            return new CurrencyAmount(
                currency: Currency.RUB,
                amount: amount);

        // Доллары в рубли.
        if (currency == Currency.USD)
            return new CurrencyAmount(
                currency: Currency.RUB,
                amount: amount * currencies.CurrencyUsdToRub);

        throw new InvalidOperationException("Unexpected currency!");
    }

    /// <summary>
    /// Конвертировать валюту в Доллары.
    /// </summary>
    /// <param name="currency">Конвертируемая валюта.</param>
    /// <param name="amount">Сумма конвертации.</param>
    /// <returns>Результат конвертации в виде суммы и валюты.</returns>
    public CurrencyAmount ToUsd(
        Currency currency, 
        decimal amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);

        // Доллары в доллары.
        if (currency == Currency.USD)
            return new CurrencyAmount(
                currency: Currency.USD,
                amount: amount);

        // Рубли в доллары.
        if (currency == Currency.RUB)
            return new CurrencyAmount(
                currency: Currency.USD,
                amount: amount / currencies.CurrencyUsdToRub);

        throw new InvalidOperationException("Unexpected currency!");
    }
}
