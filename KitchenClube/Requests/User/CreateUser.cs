namespace KitchenClube.Requests.User;

public record class CreateUser (string FullName, string PhoneNumber, string Email, string[] Roles, string Password);
