using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitchenClube.Data.TableConfigurations;

public class FoodConfigurations : IEntityTypeConfiguration<Food>
{
    public void Configure(EntityTypeBuilder<Food> builder)
    {
        builder.ToTable(nameof(Food) + "s")
            .HasKey(f => f.Id);

        builder.HasMany(f => f.MenuItems)
            .WithOne(m => m.Food)
            .HasForeignKey(m => m.FoodId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Food { Id = Guid.NewGuid(), Name = "Osh", Description = "1 ба 1", Image = "images/osh.png", IsActive = true },
            new Food { Id = Guid.NewGuid(), Name = "Kazan-Kebab", Description = "Бо гӯшти гӯспанд", Image = "images/kazan.png", IsActive = true },
            new Food { Id = Guid.NewGuid(), Name = "Jazza", Description = "Бо гӯшти гов", Image = "images/jazza.png", IsActive = true }
            );
    }
}
