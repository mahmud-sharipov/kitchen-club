using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitchenClube.Data.TableConfigurations
{
    public class UserMenuItemSelectionConfigurations : IEntityTypeConfiguration<UserMenuItemSelection>
    {
        public void Configure(EntityTypeBuilder<UserMenuItemSelection> builder)
        {
            builder.ToTable(nameof(UserMenuItemSelection) + "s")
                .HasKey(u => u.Id);

            builder.HasOne(u=>u.User)
                .WithMany(u=>u.MenuSelections)
                .HasForeignKey(u=>u.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.Menuitem)
                .WithMany(m => m.UserMenuItemSelections)
                .HasForeignKey(m => m.MenuitemId)
                .IsRequired();
        }
    }
}
