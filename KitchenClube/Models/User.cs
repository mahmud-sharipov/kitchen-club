namespace KitchenClube.Models;

public class User : IdentityUser<Guid>
{
    public string FullName { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<UserMenuitemSelection> MenuSelections { get; private set; }
}