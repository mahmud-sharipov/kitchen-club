namespace KitchenClube.Models;

public class User : BaseEntity
{
    public User()
    {
        FullName = string.Empty;
        PhoneNumber = string.Empty;
        Email = string.Empty;
        PasswordHash = string.Empty;
        IsActive = true;
        MenuSelections = new Collection<UserMenuItemSelection>();
    }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsActive { get; set; }

    public virtual Guid RoleId { get; set; }
    public virtual Role Role { get; set; }

    public virtual ICollection<UserMenuItemSelection> MenuSelections { get; private set; }
}