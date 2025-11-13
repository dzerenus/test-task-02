using Bank.Storage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Storage.Configurations;

internal class ConfigurationEntityTransaction : ConfigurationEntityBaseModel<EntityTransaction>, IEntityTypeConfiguration<EntityTransaction>
{
    public new void Configure(EntityTypeBuilder<EntityTransaction> builder)
    {
        base.Configure(builder);

        builder
            .HasOne<EntityWallet>()
            .WithMany()
            .HasForeignKey(x => x.WalletId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasIndex(x => x.WalletId);

        builder
            .HasIndex(x => new { x.WalletId, x.CreatedAtUtc });
    }
}
