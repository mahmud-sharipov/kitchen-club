namespace KitchenClube.Requests.UserMenuItemSelection
{
    public class CreateUserMenuItemSelection
    {
        public UserVote Vote { get; set; }

        public Guid MenuitemId { get; set; }        

        public Guid UserId { get; set; }
    }
}
