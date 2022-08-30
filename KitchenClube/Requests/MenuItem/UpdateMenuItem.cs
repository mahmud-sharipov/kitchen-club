namespace KitchenClube.Requests.MenuItem;

public record class UpdateMenuItem(DateTime Day, Guid FoodId, Guid MenuId, bool IsActive);
