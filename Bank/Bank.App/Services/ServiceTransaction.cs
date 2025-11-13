using Bank.App.Interfaces;
using Bank.Core.Models;
using Bank.Storage;

namespace Bank.App.Services;

internal class ServiceTransaction(
    IServiceStorage storage) 
    : IServiceTransaction
{
    public Task<int> Count(CancellationToken cancellationToken = default) 
        => storage.Transactions.Count(cancellationToken);

    public async Task<IReadOnlyList<Transaction>> Get(
        Wallet? wallet = null,
        DateTime? minCreatedAtUtc = null, 
        DateTime? maxCreatedAtUtc = null, 
        CancellationToken cancellationToken = default)
    {
        var transactions = await storage
            .Transactions
            .Get(
                wallet: wallet,
                minCreatedAtUtc: minCreatedAtUtc,
                maxCreatedAtUtc: maxCreatedAtUtc,
                cancellationToken: cancellationToken);

        return transactions;
    }

    public Task DeleteAll(CancellationToken cancellationToken = default)
        => storage.Transactions.DeleteAll(cancellationToken);
}
