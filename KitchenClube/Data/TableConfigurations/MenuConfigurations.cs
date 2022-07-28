using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitchenClube.Data.TableConfigurations;

public class MenuConfigurations : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable(nameof(Menu) + "s")
            .HasKey(m => m.Id);

        builder.HasMany(m=>m.MenuItems)
            .WithOne(m=>m.Menu)
            .HasForeignKey(m=>m.MenuId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(new Menu { Id = Guid.NewGuid(), StartDate = DateTime.Now, EndDate = DateTime.Now, Status = MenuStatus.Active });
    }
}
