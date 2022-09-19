namespace KitchenClube.Data;

public abstract class KitchenClubContext : IdentityDbContext<User,Role,Guid>
{
    public KitchenClubContext(DbContextOptions options) : base(options) { }
    public DbSet<Food> Foods { get; set; }
    public DbSet<Menu> Menu { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<UserMenuItemSelection> UserMenuItemSelections { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore(nameof(BaseEntity));
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TableConfigurations.UserConfigurations).Assembly);
    }
}