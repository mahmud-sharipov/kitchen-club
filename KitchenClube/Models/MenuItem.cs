namespace KitchenClube.Models;

public class Menuitem : BaseEntity
{
    public Menuitem()
    {
        UserMenuItemSelections = new Collection<UserMenuitemSelection>();
        IsActive = true;
    }

    public DateTime Day { get; set; }
    public bool IsActive { get; set; }

    public Guid FoodId { get; set; }
    public virtual Food Food { get; set; }

    public Guid MenuId { get; set; }
    public virtual Menu Menu { get; set; }

    public virtual ICollection<UserMenuitemSelection> UserMenuItemSelections { get; private set; }
}
