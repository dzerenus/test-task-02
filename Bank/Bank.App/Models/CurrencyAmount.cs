using Bank.Core.Enums;

namespace Bank.App.Models;

public class CurrencyAmount(
    Currency currency, 
    decimal amount)
{
    public Currency Currency { get; } = currency;

    public decimal Amount { get; } = amount;
}
