namespace KitchenClube.Models;

public class Food : BaseEntity
{
    public Food()
    {
        Name = String.Empty;
        Image = String.Empty;
        Description = String.Empty;
        MenuItems = new Collection<Menuitem>();
    }

    public string Name { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<Menuitem> MenuItems { get; private set; }
}
