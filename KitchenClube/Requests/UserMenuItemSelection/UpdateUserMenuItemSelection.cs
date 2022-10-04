namespace KitchenClube.Requests.UserMenuItemSelection;

public record class UpdateUserMenuitemSelection(UserVote Vote, Guid MenuitemId, Guid UserId);
