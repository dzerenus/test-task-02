using Microsoft.EntityFrameworkCore;

namespace Bank.Storage;

/// <summary>
/// Фабрика формирования контектов Entity Framework.
/// </summary>
/// <param name="connectionString">Строка подключения к базе данных (PostgreSQL).</param>
internal class DatabaseContextFactory(string connectionString) : IDbContextFactory<DatabaseContext>
{
    /// <summary>
    /// Создать контекст базы данных.
    /// </summary>
    /// <returns>Контекст базы даных.</returns>
    public DatabaseContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseNpgsql(connectionString);

        var options = optionsBuilder.Options;
        return new DatabaseContext(options);
    }
}
