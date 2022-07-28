namespace KitchenClube.Responses;
public record UserMenuItemSelectionResponse(Guid Id, Guid MenuitemId, Guid UserId, UserVote Vote);