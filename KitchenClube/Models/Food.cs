namespace KitchenClube.Models;

public class Food : BaseEntity
{
    public Food()
    {
        Name = String.Empty;
        Image = String.Empty;
        Description = String.Empty;
        MenuItems = new Collection<MenuItem>();
    }

    public string Name { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<MenuItem> MenuItems { get; private set; }
}
