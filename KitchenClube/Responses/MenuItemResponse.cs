namespace KitchenClube.Responses;

public record MenuItemResponse(Guid Id, DateTime Day, Guid FoodId, Guid MenuId, bool IsActive);
