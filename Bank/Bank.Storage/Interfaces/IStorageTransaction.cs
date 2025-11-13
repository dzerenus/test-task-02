using Bank.Core.Models;

namespace Bank.Storage.Interfaces;

public interface IStorageTransaction
{
    public Task<int> Count(CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Transaction>> Get(
        Wallet? wallet = null,
        DateTime? minCreatedAtUtc = null,
        DateTime? maxCreatedAtUtc = null,
        CancellationToken cancellationToken = default);

    public Task Insert(
        IReadOnlyList<Transaction> transactions, 
        CancellationToken cancellationToken = default);

    public Task DeleteAll(CancellationToken cancellationToken = default);
}
