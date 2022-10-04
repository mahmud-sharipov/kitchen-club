namespace KitchenClube.Responses;
public record UserMenuitemSelectionResponse(Guid Id, Guid MenuitemId, Guid UserId, UserVote Vote);