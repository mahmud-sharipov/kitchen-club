namespace KitchenClube.Requests.MenuItem;

public record class CreateMenuitem(DateTime Day, Guid FoodId, Guid MenuId);