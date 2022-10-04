namespace KitchenClube.Models;

public class Menu : BaseEntity
{
    public Menu()
    {
        Status = MenuStatus.Draft;
        MenuItems = new Collection<Menuitem>();
    }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public MenuStatus Status { get; set; }

    public virtual ICollection<Menuitem> MenuItems { get; private set; }
}