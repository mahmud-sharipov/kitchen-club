namespace KitchenClube.Data;

public abstract class KitchenClubContext : DbContext
{
    public KitchenClubContext(DbContextOptions options) : base(options) { }
    public DbSet<Food> Foods { get; set; }
    public DbSet<Menu> Menu { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<UserMenuItemSelection> UserMenuItemSelections { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore(nameof(BaseEntity));
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TableConfigurations.UserConfigurations).Assembly);
    }
}