using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitchenClube.Data.TableConfigurations
{
    public class UserMenuItemSelectionConfigurations : IEntityTypeConfiguration<UserMenuitemSelection>
    {
        public void Configure(EntityTypeBuilder<UserMenuitemSelection> builder)
        {
            builder.ToTable(nameof(UserMenuitemSelection) + "s")
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
