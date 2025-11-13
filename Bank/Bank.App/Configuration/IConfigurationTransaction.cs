namespace Bank.App.Configuration;

public interface IConfigurationTransaction
{
    public int MinTransactionAmountRub { get; }

    public int MaxTransactionAmountRub { get; }

    public int MinTransactionAmountUsd { get; }

    public int MaxTransactionAmountUsd { get; }
}
