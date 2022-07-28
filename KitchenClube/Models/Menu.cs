namespace KitchenClube.Models;

public class Menu : BaseEntity
{
    public Menu()
    {
        Status = MenuStatus.Draft;
        MenuItems = new Collection<MenuItem>();
    }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public MenuStatus Status { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<MenuItem> MenuItems { get; private set; }
}