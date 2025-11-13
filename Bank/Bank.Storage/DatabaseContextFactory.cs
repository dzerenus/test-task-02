using Microsoft.EntityFrameworkCore;

namespace Bank.Storage;

internal class DatabaseContextFactory(string connectionString) : IDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseNpgsql(connectionString);

        var options = optionsBuilder.Options;
        return new DatabaseContext(options);
    }
}
