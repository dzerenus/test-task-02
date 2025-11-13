using Bank.App.Models;
using Bank.Core.Enums;

namespace Bank.App.Interfaces;

public interface IServiceExchange
{
    public CurrencyAmount ToRub(Currency currency, decimal amount);

    public CurrencyAmount ToUsd(Currency currency, decimal amount);
}
