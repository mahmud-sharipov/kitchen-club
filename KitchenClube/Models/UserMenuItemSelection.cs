namespace KitchenClube.Models;

public class UserMenuitemSelection : BaseEntity
{
    public UserMenuitemSelection()
    {
        Vote = UserVote.Yes;
    }

    public UserVote Vote { get; set; }

    public Guid MenuitemId { get; set; }
    public virtual Menuitem Menuitem { get; set; }

    public Guid UserId { get; set; }    
    public virtual User User { get; set; }
}