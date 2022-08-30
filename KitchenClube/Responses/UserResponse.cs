namespace KitchenClube.Responses;
public record UserResponse(Guid Id,string FullName, string PhoneNumber, string Email, Guid RoleId, bool IsActive);