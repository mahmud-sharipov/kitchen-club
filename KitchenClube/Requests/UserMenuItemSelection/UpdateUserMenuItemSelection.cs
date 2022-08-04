namespace KitchenClube.Requests.UserMenuItemSelection
{
    public class UpdateUserMenuItemSelection
    {
        public UserVote Vote { get; set; }

        public Guid MenuitemId { get; set; }

        public Guid UserId { get; set; }
    }
}
