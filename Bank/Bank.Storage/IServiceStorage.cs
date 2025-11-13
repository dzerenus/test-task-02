using Bank.Storage.Interfaces;

namespace Bank.Storage;

public interface IServiceStorage
{
    public IStorageWallet Wallets { get; }

    public IStorageTransaction Transactions { get; }
}
