namespace KitchenClube.Data;

public class KitchenClubSqlServerContext : KitchenClubContext
{
    public KitchenClubSqlServerContext(DbContextOptions<KitchenClubSqlServerContext> options) : base(options) { }
}
