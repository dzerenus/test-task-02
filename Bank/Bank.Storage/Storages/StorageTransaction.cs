using Bank.Core.Models;
using Bank.Storage.Entities;
using Bank.Storage.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bank.Storage.Storages;

internal class StorageTransaction(IDbContextFactory<DatabaseContext> dbContextFactory) : IStorageTransaction
{
    public async Task<int> Count(CancellationToken cancellationToken = default)
    {
        using var context = dbContextFactory.CreateDbContext();

        var count = await context
            .Transactions
            .CountAsync(cancellationToken);

        return count;
    }

    public async Task<IReadOnlyList<Transaction>> Get(
        Wallet? wallet = null,
        DateTime? minCreatedAtUtc = null,
        DateTime? maxCreatedAtUtc = null,
        CancellationToken cancellationToken = default)
    {
        using var context = dbContextFactory.CreateDbContext();

        var query = context
            .Transactions
            .AsNoTracking();

        if (wallet is not null)
            query = query.Where(x => x.WalletId == wallet.Id);

        if (minCreatedAtUtc is not null)
            query = query.Where(x => x.CreatedAtUtc >= minCreatedAtUtc.Value);

        if (maxCreatedAtUtc is not null)
            query = query.Where(x => x.CreatedAtUtc <= maxCreatedAtUtc.Value);

        var entities = await query
            .OrderBy(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        var models = entities
            .Select(x => x.GetModel())
            .ToList();

        return models;
    }

    public async Task Insert(IReadOnlyList<Transaction> transactions, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(transactions);

        using var context = dbContextFactory.CreateDbContext();

        var entities = transactions
            .Select(x => new EntityTransaction(x))
            .ToList();

        context
            .Transactions
            .AddRange(entities);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAll(CancellationToken cancellationToken = default)
    {
        using var context = dbContextFactory.CreateDbContext();

        await context
            .Transactions
            .ExecuteDeleteAsync(cancellationToken);
    }
}
