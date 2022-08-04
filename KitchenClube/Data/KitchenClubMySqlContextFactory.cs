using Microsoft.EntityFrameworkCore.Design;

namespace KitchenClube.Data;

public abstract class KitchenClubMySqlContextFactory : IDesignTimeDbContextFactory<KitchenClubMySqlContext>
{
    public KitchenClubMySqlContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile($"dbsettings.Development.json", optional: true)
              .AddJsonFile($"appsettings.json", optional: true)
              .AddEnvironmentVariables()
              .Build();

        var optionsBuilder = new DbContextOptionsBuilder<KitchenClubMySqlContext>();
        optionsBuilder.UseMySql(configuration.GetConnectionString("mysql"), ServerVersion.Parse("8.0.0-mysql"));
        optionsBuilder.UseLazyLoadingProxies();
        return new KitchenClubMySqlContext(optionsBuilder.Options);
    }
}
