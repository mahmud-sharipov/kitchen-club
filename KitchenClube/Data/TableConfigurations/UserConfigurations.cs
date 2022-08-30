using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitchenClube.Data.TableConfigurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User) + "s")
            .HasKey(u => u.Id);

        builder.Ignore(b => b.MenuSelections);

        builder.HasMany(u => u.MenuSelections)
            .WithOne(m => m.User)
            .HasForeignKey(u => u.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasData(new User { Id = Guid.NewGuid() ,FullName = "Azizjon", Email = "azizjon@gmail.com", PhoneNumber = "+992 92 929 2992", IsActive = true, PasswordHash = "UDٳ�#��)����d#� ��VB�~j_̓�ݿ��������!w�K��S�D��H���jI�n�	�&�" },
                         new User { Id = Guid.NewGuid(), FullName = "Amirjon", Email = "amirjon@gmail.com", PhoneNumber = "+992 92 777 00 77", IsActive = true, PasswordHash = "UDٳ�#��)����d#� ��VB�~j_̓�ݿ��������!w�K��S�D��H���jI�n�	�&�" },
                         new User { Id = Guid.NewGuid(), FullName = "Karimjon", Email = "karimjon@gamil.com", PhoneNumber = "+992 92 888 77 66", IsActive = true, PasswordHash = "UDٳ�#��)����d#� ��VB�~j_̓�ݿ��������!w�K��S�D��H���jI�n�	�&�" });
    }
}