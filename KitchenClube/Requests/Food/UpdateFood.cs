namespace KitchenClube.Requests.Food;

public record class UpdateFood(string Name, string Image, string Description, bool IsActive);
