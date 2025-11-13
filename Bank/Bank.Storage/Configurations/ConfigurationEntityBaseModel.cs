using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bank.Storage.Entities;

namespace Bank.Storage.Configurations;

/// <summary>
/// Настройки базовой сущности в базе данных.
/// </summary>
/// <typeparam name="T">Базовая сущность базы данных.</typeparam>
internal class ConfigurationEntityBaseModel<T> : IEntityTypeConfiguration<T> where T : EntityBaseModel
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        // Устанавлиаем первичный ключ.
        builder
            .HasKey(x => x.Id);

        // Индекс даты создания по возрастанию.
        builder
            .HasIndex(x => x.CreatedAtUtc);

        // Индекс даты изменения по возрастанию.
        builder
            .HasIndex(x => x.UpdatedAtUtc);
    }
}
