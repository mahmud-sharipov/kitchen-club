namespace KitchenClube.Controllers.V2;

[Route("api/{v:apiVersion}/menuitems/")]
[ApiController]
[ApiVersion("2.0")]
[Authorize]
public class MenuItemsController : ControllerBase
{
    private readonly Services.V2.IMenuItemService _menuItemService;
 
    public MenuItemsController(Services.V2.IMenuItemService menuItemService)
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

    [HttpPut("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutMenuItem(Guid id, UpdateMenuItem updateMenuItem)
    {
        await _menuItemService.UpdateAsync(id, updateMenuItem);
        return NoContent();
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<ActionResult<MenuItem>> PostMenuItem(CreateMenuItem createMenuItem)
    {
        var menuItem = await _menuItemService.CreateAsync(createMenuItem);
        return CreatedAtAction("GetMenuItem", new { id = menuItem.Id }, menuItem);
    }

    [HttpDelete("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMenuItem(Guid id)
    {
        await _menuItemService.DeleteAsync(id);
        return NoContent();
    }
}
