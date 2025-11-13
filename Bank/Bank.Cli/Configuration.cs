using Bank.App.Configuration;

namespace Bank.Cli;

internal class Configuration(
    string storageConnectionString, 
    int minTransactionAmountRub,
    int maxTransactionAmountRub,
    int minTransactionAmountUsd,
    int maxTransactionAmountUsd, 
    decimal currencyUsdToRub)
    : IConfigurationApp
{
    public string StorageConnectionString { get; } = storageConnectionString;

    public int MinTransactionAmountRub { get; } = minTransactionAmountRub;

    public int MaxTransactionAmountRub { get; } = maxTransactionAmountRub;

    public int MinTransactionAmountUsd { get; } = minTransactionAmountUsd;

    public int MaxTransactionAmountUsd { get; } = maxTransactionAmountUsd;

    public decimal CurrencyUsdToRub { get; } = currencyUsdToRub;
}
