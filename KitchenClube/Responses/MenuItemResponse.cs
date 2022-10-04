namespace KitchenClube.Responses;

public record MenuitemResponse(Guid Id, DateTime Day, Guid FoodId, Guid MenuId, bool IsActive);
