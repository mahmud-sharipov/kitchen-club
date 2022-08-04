namespace KitchenClube.Data;

public class KitchenClubMySqlContext : KitchenClubContext
{
    public KitchenClubMySqlContext(DbContextOptions<KitchenClubMySqlContext> options) : base(options) { }
}
