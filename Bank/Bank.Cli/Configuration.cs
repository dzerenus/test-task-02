using Bank.App.Configuration;

namespace Bank.Cli;

/// <summary>
/// Общая конфигурация программы.
/// </summary>
/// <param name="storageConnectionString">Строка для подключения к базе данных.</param>
/// <param name="minTransactionAmountRub">Минимальный размер транзакции в рублях.</param>
/// <param name="maxTransactionAmountRub">Максимальный размер транзакции в рублях.</param>
/// <param name="minTransactionAmountUsd">Минимальный размер транзакции в долларах.</param>
/// <param name="maxTransactionAmountUsd">Максимальный размер транзакции в долларах.</param>
/// <param name="currencyUsdToRub">Количество рублей, которые можно получить за один доллар.</param>
internal class Configuration(
    string storageConnectionString, 
    int minTransactionAmountRub,
    int maxTransactionAmountRub,
    int minTransactionAmountUsd,
    int maxTransactionAmountUsd, 
    decimal currencyUsdToRub)
    : IConfigurationApp
{
    /// <summary>
    /// Строка для подключения к базе данных.
    /// </summary>
    public string StorageConnectionString { get; } = storageConnectionString;

    /// <summary>
    /// Минимальный размер транзакции в рублях.
    /// </summary>
    public int MinTransactionAmountRub { get; } = minTransactionAmountRub;

    /// <summary>
    /// Максимальный размер транзакции в рублях.
    /// </summary>
    public int MaxTransactionAmountRub { get; } = maxTransactionAmountRub;

    /// <summary>
    /// Минимальный размер транзакции в долларах.
    /// </summary>
    public int MinTransactionAmountUsd { get; } = minTransactionAmountUsd;

    /// <summary>
    /// Максимальный размер транзакции в долларах.
    /// </summary>
    public int MaxTransactionAmountUsd { get; } = maxTransactionAmountUsd;

    /// <summary>
    /// Количество рублей, которые можно получить за один доллар.
    /// </summary>
    public decimal CurrencyUsdToRub { get; } = currencyUsdToRub;
}
