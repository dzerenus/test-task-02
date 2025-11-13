using Bank.Core.Models;

namespace Bank.App.Interfaces;

public interface IServiceTransaction
{
    public Task<int> Count(CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Transaction>> Get(
        Wallet? wallet = null,
        DateTime? minCreatedAtUtc = null,
        DateTime? maxCreatedAtUtc = null,
        CancellationToken cancellationToken = default);

    public Task DeleteAll(CancellationToken cancellationToken = default);
}
