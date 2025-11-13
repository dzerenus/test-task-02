using Bank.Core.Models;

namespace Bank.Storage.Interfaces;

public interface IStorageWallet
{
    public Task<int> Count(CancellationToken cancellationToken = default);

    public Task<Wallet?> Get(Guid id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Wallet>> Get(CancellationToken cancellationToken = default);

    public Task Insert(IReadOnlyList<Wallet> wallets, CancellationToken cancellationToken = default);

    public Task DeleteAll(CancellationToken cancellationToken = default);
}
