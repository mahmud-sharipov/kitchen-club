namespace KitchenClube.Models;
public class Role:BaseEntity
{
    public string Name { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<User> Users { get; private set; }
}
