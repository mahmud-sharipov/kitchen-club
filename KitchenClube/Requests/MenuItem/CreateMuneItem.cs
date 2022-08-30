namespace KitchenClube.Requests.MenuItem;

public record class CreateMenuItem(DateTime Day, Guid FoodId, Guid MenuId);