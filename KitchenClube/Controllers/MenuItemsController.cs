namespace KitchenClube.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenuItemsController : ControllerBase
{
    private readonly IMenuItemService _menuItemService;
 
    public MenuItemsController(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;        
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuItemResponse>>> GetMenuItems()
    {
        return Ok(await _menuItemService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuItemResponse>> GetMenuItem(Guid id)
    {
        return Ok(await _menuItemService.GetAsync(id));
    }

   
    [HttpGet("menu/{menuId}")]
    public async Task<ActionResult<IEnumerable<MenuItemResponse>>> GetMenuItemsByMenuId(Guid menuId)
    {
        return Ok(await _menuItemService.MenuItemsByMenuId(menuId));
    }

    [HttpGet("food/{foodId}")]
    public async Task<ActionResult<IEnumerable<MenuItemResponse>>> GetMenuItemsByFoodId(Guid foodId)
    {
        return Ok(await _menuItemService.MenuItemsByFoodId(foodId));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMenuItem(Guid id, UpdateMenuItem updateMenuItem)
    {
        await _menuItemService.UpdateAsync(id, updateMenuItem);
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<MenuItem>> PostMenuItem(CreateMenuItem createMenuItem)
    {
        var menuItem = await _menuItemService.CreateAsync(createMenuItem);
        return CreatedAtAction("GetMenuItem", new { id = menuItem.Id }, menuItem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItem(Guid id)
    {
        await _menuItemService.DeleteAsync(id);
        return NoContent();
    }
}
