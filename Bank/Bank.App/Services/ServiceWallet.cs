using Bank.App.Interfaces;
using Bank.Core.Models;
using Bank.Storage;

namespace Bank.App.Services;

internal class ServiceWallet(IServiceStorage storage) : IServiceWallet
{
    public async Task<int> Count(CancellationToken cancellationToken = default) 
        => await storage.Wallets.Count(cancellationToken);

    public async Task<IReadOnlyList<Wallet>> Get(CancellationToken cancellationToken = default)
        => await storage.Wallets.Get(cancellationToken);

    public async Task<Wallet?> Get(Guid id, CancellationToken cancellationToken = default)
        => await storage.Wallets.Get(id, cancellationToken);

    public async Task DeleteAll(CancellationToken cancellationToken = default)
        => await storage.Wallets.DeleteAll(cancellationToken);
}
