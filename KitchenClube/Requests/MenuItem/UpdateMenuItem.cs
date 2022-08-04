namespace KitchenClube.Requests.MenuItem;

public class UpdateMenuItemRequest
{
    public DateTime Day { get; set; }
    public Guid FoodId { get; set; }
    public Guid MenuId { get; set; }
    public bool IsActive { get; set; }

}
