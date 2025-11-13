using Bank.Storage.Interfaces;
using Bank.Storage.Storages;

namespace Bank.Storage;

/// <summary>
/// Реализация сервиса хранилища.
/// </summary>
public class ServiceStorage : IServiceStorage
{
    /// <summary>
    /// Сервис управления транзакциями.
    /// </summary>
    public IStorageTransaction Transactions { get; }

    /// <summary>
    /// Сервис управления кошельками.
    /// </summary>
    public IStorageWallet Wallets { get; }

    public ServiceStorage(IConfigurationStorage configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentException.ThrowIfNullOrWhiteSpace(configuration.StorageConnectionString);

        var dbContextFactory = new DatabaseContextFactory(configuration.StorageConnectionString);
        Transactions = new StorageTransaction(dbContextFactory);
        Wallets = new StorageWallet(dbContextFactory);
    }
}
