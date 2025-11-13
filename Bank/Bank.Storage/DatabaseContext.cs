using Microsoft.EntityFrameworkCore;
using Bank.Storage.Configurations;
using Bank.Storage.Entities;

namespace Bank.Storage;

internal class DatabaseContext : DbContext
{
    public DbSet<EntityTransaction> Transactions => Set<EntityTransaction>();

    public DbSet<EntityWallet> Wallets => Set<EntityWallet>();

    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<EntityWallet>(new ConfigurationEntityWallet());
        modelBuilder.ApplyConfiguration<EntityTransaction>(new ConfigurationEntityTransaction());
    }
}
