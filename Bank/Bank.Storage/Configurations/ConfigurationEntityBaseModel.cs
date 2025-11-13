using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bank.Storage.Entities;

namespace Bank.Storage.Configurations;

internal class ConfigurationEntityBaseModel<T> : IEntityTypeConfiguration<T> where T : EntityBaseModel
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasIndex(x => x.CreatedAtUtc);

        builder
            .HasIndex(x => x.UpdatedAtUtc);
    }
}
