using Bank.Core.Models;
using Bank.Storage.Entities;
using Bank.Storage.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bank.Storage.Storages;

internal class StorageWallet(IDbContextFactory<DatabaseContext> dbContextFactory) : IStorageWallet
{
    public async Task<int> Count(CancellationToken cancellationToken = default)
    {
        using var context = dbContextFactory.CreateDbContext();

        var count = await context
            .Wallets
            .CountAsync(cancellationToken);

        return count;
    }

    public async Task<Wallet?> Get(Guid id, CancellationToken cancellationToken = default)
    {
        using var context = dbContextFactory.CreateDbContext();

        var entity = await context
            .Wallets
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity?.GetModel();
    }

    public async Task<IReadOnlyList<Wallet>> Get(CancellationToken cancellationToken = default)
    {
        using var context = dbContextFactory.CreateDbContext();

        var entities = await context
            .Wallets
            .AsNoTracking()
            .OrderBy(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        var models = entities
            .Select(x => x.GetModel())
            .ToList();

        return models;
    }

    public async Task Insert(
        IReadOnlyList<Wallet> wallets, 
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(wallets);

        using var context = dbContextFactory.CreateDbContext();

        var entities = wallets
            .Select(x => new EntityWallet(x))
            .ToList();

        context.Wallets.AddRange(entities);

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAll(CancellationToken cancellationToken = default)
    {
        using var context = dbContextFactory.CreateDbContext();

        await context
            .Wallets
            .ExecuteDeleteAsync(cancellationToken);
    }
}
