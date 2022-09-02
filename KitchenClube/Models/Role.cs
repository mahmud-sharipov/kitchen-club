namespace KitchenClube.Models;
public class Role: IdentityRole<Guid>
{
    public Role(string Name) : base(Name){}
    public bool IsActive { get; set; }
}
