namespace Bank.App.Configuration;

/// <summary>
/// Конфигурация максимальных и минимальных возможных транзакций для разных валют.
/// </summary>
public interface IConfigurationTransaction
{
    /// <summary>
    /// Минимальный размер транзакции в рублях.
    /// </summary>
    public int MinTransactionAmountRub { get; }

    /// <summary>
    /// Максимальный размер транзакции в рублях.
    /// </summary>
    public int MaxTransactionAmountRub { get; }

    /// <summary>
    /// Минимальный размер транзакции в долларах.
    /// </summary>
    public int MinTransactionAmountUsd { get; }

    /// <summary>
    /// Максимальный размер транзакции в долларах.
    /// </summary>
    public int MaxTransactionAmountUsd { get; }
}
