namespace KitchenClube.Controllers;

[Route("api/{v:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class MenuitemsController : ControllerBase
{
    private readonly IMenuitemService _menuitemService;
 
    public MenuitemsController(IMenuitemService menuitemService)
    {
        _menuitemService = menuitemService;        
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuitemResponse>>> GetMenuitems()
    {
        return Ok(await _menuitemService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuitemResponse>> GetMenuitem(Guid id)
    {
        return Ok(await _menuitemService.GetAsync(id));
    }
   
    [HttpGet("menu/{menuId}")]
    public async Task<ActionResult<IEnumerable<MenuitemResponse>>> GetMenuitemsByMenuId(Guid menuId)
    {
        return Ok(await _menuitemService.GetMenuitemsByMenuId(menuId));
    }

    [HttpGet("food/{foodId}")]
    public async Task<ActionResult<IEnumerable<MenuitemResponse>>> GetMenuitemsByFoodId(Guid foodId)
    {
        return Ok(await _menuitemService.GetMenuitemsByFoodId(foodId));
    }

    [HttpPut("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutMenuitem(Guid id, UpdateMenuitem updateMenuitem)
    {
        await _menuitemService.UpdateAsync(id, updateMenuitem);
        return NoContent();
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<ActionResult<Menuitem>> PostMenuitem(CreateMenuitem createMenuitem)
    {
        var menuitem = await _menuitemService.CreateAsync(createMenuitem);
        return CreatedAtAction("GetMenuitem", new { id = menuitem.Id }, menuitem);
    }

    [HttpDelete("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMenuitem(Guid id)
    {
        await _menuitemService.DeleteAsync(id);
        return NoContent();
    }
}
