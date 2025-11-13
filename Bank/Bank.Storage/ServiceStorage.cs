using Bank.Storage.Interfaces;
using Bank.Storage.Storages;

namespace Bank.Storage;

public class ServiceStorage : IServiceStorage
{
    public IStorageTransaction Transactions { get; }

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
