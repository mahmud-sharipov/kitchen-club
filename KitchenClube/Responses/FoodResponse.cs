namespace KitchenClube.Responses;

public record FoodResponse (Guid Id, string Name, string Image, string Description, bool IsActive);
