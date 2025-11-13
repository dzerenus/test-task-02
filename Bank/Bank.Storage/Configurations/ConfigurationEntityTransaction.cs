using Bank.Storage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Storage.Configurations;

/// <summary>
/// Настройки сущности транзакции в базе данных.
/// </summary>
internal class ConfigurationEntityTransaction : ConfigurationEntityBaseModel<EntityTransaction>, IEntityTypeConfiguration<EntityTransaction>
{
    public new void Configure(EntityTypeBuilder<EntityTransaction> builder)
    {
        base.Configure(builder);

        // Внешний ключ к кошельку с каскадным удалением.

        builder
            .HasOne<EntityWallet>()
            .WithMany()
            .HasForeignKey(x => x.WalletId)
            .OnDelete(DeleteBehavior.Cascade);

        // Индекс по ID кошелька.
        builder
            .HasIndex(x => x.WalletId);

        // Составной индекс по ID кошелька и дате создания по возрастанию.
        builder
            .HasIndex(x => new { x.WalletId, x.CreatedAtUtc });
    }
}
