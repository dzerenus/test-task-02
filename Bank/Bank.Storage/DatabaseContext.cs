using Microsoft.EntityFrameworkCore;
using Bank.Storage.Configurations;
using Bank.Storage.Entities;

namespace Bank.Storage;

/// <summary>
/// Контекст базы даных
/// </summary>
internal class DatabaseContext : DbContext
{
    /// <summary>
    /// Таблица транзакций.
    /// </summary>
    public DbSet<EntityTransaction> Transactions => Set<EntityTransaction>();

    /// <summary>
    /// Таблица кошельков.
    /// </summary>
    public DbSet<EntityWallet> Wallets => Set<EntityWallet>();

    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Применяем настройки сущностей.

        modelBuilder.ApplyConfiguration<EntityWallet>(new ConfigurationEntityWallet());
        modelBuilder.ApplyConfiguration<EntityTransaction>(new ConfigurationEntityTransaction());
    }
}
