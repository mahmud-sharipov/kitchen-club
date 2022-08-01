namespace KitchenClube.Models;

public class UserMenuItemSelection : BaseEntity
{
    public UserMenuItemSelection()
    {
        Vote = UserVote.Yes;
    }

    public UserVote Vote { get; set; }

    public Guid MenuitemId { get; set; }
    public virtual MenuItem Menuitem { get; set; }

    public Guid UserId { get; set; }    
    public virtual User User { get; set; }
}