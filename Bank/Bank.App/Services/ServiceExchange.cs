using Bank.App.Configuration;
using Bank.App.Interfaces;
using Bank.App.Models;
using Bank.Core.Enums;

namespace Bank.App.Services;

internal class ServiceExchange(IConfugurationCurrencies currencies) : IServiceExchange
{
    public CurrencyAmount ToRub(Currency currency, decimal amount)
    {
        if (currency == Currency.RUB)
            return new CurrencyAmount(
                currency: Currency.RUB,
                amount: amount);

        if (currency == Currency.USD)
            return new CurrencyAmount(
                currency: Currency.RUB,
                amount: amount * currencies.CurrencyUsdToRub);

        throw new InvalidOperationException("Unexpected currency!");
    }

    public CurrencyAmount ToUsd(Currency currency, decimal amount)
    {
        if (currency == Currency.USD)
            return new CurrencyAmount(
                currency: Currency.USD,
                amount: amount);

        if (currency == Currency.RUB)
            return new CurrencyAmount(
                currency: Currency.USD,
                amount: amount / currencies.CurrencyUsdToRub);

        throw new InvalidOperationException("Unexpected currency!");
    }
}
