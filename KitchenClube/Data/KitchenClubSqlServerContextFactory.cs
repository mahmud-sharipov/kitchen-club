using Microsoft.EntityFrameworkCore.Design;

namespace KitchenClube.Data;

public abstract class KitchenClubSqlServerContextFactory : IDesignTimeDbContextFactory<KitchenClubSqlServerContext>
{
    public KitchenClubSqlServerContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile($"dbsettings.Development.json", optional: true)
              .AddJsonFile($"appsettings.json", optional: true)
              .AddEnvironmentVariables()
              .Build();

        var optionsBuilder = new DbContextOptionsBuilder<KitchenClubSqlServerContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("sqlserver"));
        optionsBuilder.UseLazyLoadingProxies();
        return new KitchenClubSqlServerContext(optionsBuilder.Options);
    }
}
