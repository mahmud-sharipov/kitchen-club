using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitchenClube.Data.TableConfigurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Ignore(b => b.MenuSelections);

        builder.HasMany(u => u.MenuSelections)
            .WithOne(m => m.User)
            .HasForeignKey(u => u.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}