using Bank.Storage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bank.Storage.Configurations;

internal class ConfigurationEntityWallet : ConfigurationEntityBaseModel<EntityWallet>, IEntityTypeConfiguration<EntityWallet>
{
    public new void Configure(EntityTypeBuilder<EntityWallet> builder)
    {
        base.Configure(builder);
    }
}
