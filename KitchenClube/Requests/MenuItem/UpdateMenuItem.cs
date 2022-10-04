namespace KitchenClube.Requests.MenuItem;

public record class UpdateMenuitem(DateTime Day, Guid FoodId, Guid MenuId, bool IsActive);
