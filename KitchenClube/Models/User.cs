namespace KitchenClube.Models;

public class User : IdentityUser<Guid>
{
    public string FullName { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<UserMenuItemSelection> MenuSelections { get; private set; }
}