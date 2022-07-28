using KitchenClube.Data;
using KitchenClube.Requests.MenuItem;

namespace KitchenClube.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenuItemsController : ControllerBase
{
    private readonly KitchenClubContext _context;

    public MenuItemsController(KitchenClubContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItems()
    {
        return await _context.MenuItems.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuItem>> GetMenuItem(Guid id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);

        if (menuItem == null)
            return NotFound();

        return menuItem;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMenuItem(Guid id, UpdateMenuItemRequest menuItemRequest)
    {
        var menuItem = _context.MenuItems.FirstOrDefault(x => x.Id == id);
        if (menuItem is null)
            return NotFound();

        var food = _context.Foods.FirstOrDefault(x => x.Id == menuItemRequest.FoodId);
        if (food is null)
            return NotFound(menuItemRequest.FoodId);

        var menu = _context.Menu.FirstOrDefault(x => x.Id == menuItemRequest.MenuId);
        if (menu is null)
            return NotFound(menuItemRequest.MenuId);

        if (menuItemRequest.Day > menu.EndDate || menuItemRequest.Day < menu.StartDate)
            throw new Exception("Date is out of menu period!!!");

        menuItem.Food = food;
        menuItem.Menu = menu;
        menuItem.Day = menuItemRequest.Day;
        menuItem.IsActive = menuItemRequest.IsActive;
        _context.MenuItems.Update(menuItem);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<MenuItem>> PostMenuItem(CreateMenuItemRequest menuItemRequest)
    {
        var menu = _context.Menu.FirstOrDefault(x => x.Id == menuItemRequest.MenuId);
        if (menu is null)
            return NotFound(menuItemRequest.MenuId);

        var food = _context.Foods.FirstOrDefault(x => x.Id == menuItemRequest.FoodId);
        if (food is null)
            return NotFound(menuItemRequest.FoodId);

        if (menuItemRequest.Day > menu.EndDate || menuItemRequest.Day < menu.StartDate)
            throw new Exception("Date is out of menu period!!!");

        var menuItem = new MenuItem() {
            IsActive = true,
            Food = food,
            Menu = menu,
            Day = menuItemRequest.Day,
        };
        _context.MenuItems.Add(menuItem);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetMenuItem", new { id = menuItem.Id }, menuItem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItem(Guid id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);
        if (menuItem == null) {
            return NotFound();
        }

        _context.MenuItems.Remove(menuItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
