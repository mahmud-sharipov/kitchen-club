using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitchenClube.Data.TableConfigurations;
public class RoleConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(nameof(Role) + "s")
            .HasKey(u => u.Id);

        builder.Ignore(r=>r.Users);

        builder.HasMany(r=>r.Users)
            .WithOne(u=>u.Role)
            .HasForeignKey(u => u.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
