namespace KitchenClube.Requests.UserMenuItemSelection;

public record class UpdateUserMenuItemSelection(UserVote Vote, Guid MenuitemId, Guid UserId);
