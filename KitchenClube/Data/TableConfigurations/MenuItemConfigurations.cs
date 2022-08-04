using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitchenClube.Data.TableConfigurations;

public class MenuItemConfigurations : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable(nameof(MenuItem) + "s")
            .HasKey(m => m.Id);

        builder.HasOne(m => m.Food)
            .WithMany(f => f.MenuItems)
            .HasForeignKey(m => m.FoodId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Menu)
            .WithMany(m => m.MenuItems)
            .HasForeignKey(m => m.MenuId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
