namespace KitchenClube.Requests.MenuItem;

public class CreateMenuItemRequest
{
    public DateTime Day { get; set; }
    public Guid FoodId { get; set; }
    public Guid MenuId { get; set; }
}
