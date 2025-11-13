using Bank.Core.Models;

namespace Bank.App.Interfaces;

public interface IServiceWallet
{
    public Task<int> Count(CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Wallet>> Get(CancellationToken cancellationToken = default);

    public Task<Wallet?> Get(Guid id, CancellationToken cancellationToken = default);

    public Task DeleteAll(CancellationToken cancellationToken = default);
}
