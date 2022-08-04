namespace KitchenClube.Models;

public class MenuItem : BaseEntity
{
    public MenuItem()
    {
        UserMenuItemSelections = new Collection<UserMenuItemSelection>();
        IsActive = true;
    }

    public DateTime Day { get; set; }
    public bool IsActive { get; set; }

    public Guid FoodId { get; set; }
    public virtual Food Food { get; set; }

    public Guid MenuId { get; set; }
    public virtual Menu Menu { get; set; }

    public virtual ICollection<UserMenuItemSelection> UserMenuItemSelections { get; private set; }
}
