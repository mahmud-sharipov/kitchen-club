namespace KitchenClube.Requests.User;

public record class UpdateUser(string FullName, string PhoneNumber, bool IsActive);
