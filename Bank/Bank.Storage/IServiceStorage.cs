using Bank.Storage.Interfaces;

namespace Bank.Storage;

/// <summary>
/// Сервис управления хранилищем.
/// </summary>
public interface IServiceStorage
{
    /// <summary>
    /// Сервис управления транзакциями.
    /// </summary>
    public IStorageTransaction Transactions { get; }

    /// <summary>
    /// Сервис управления кошельками.
    /// </summary>
    public IStorageWallet Wallets { get; }
}
