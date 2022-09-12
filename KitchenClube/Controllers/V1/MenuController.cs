namespace KitchenClube.Controllers.V1;

[Route("api/{v:apiVersion}/menu/")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class MenuController : ControllerBase
{
    private readonly Services.V1.IMenuService _menuService;
    public MenuController(Services.V1.IMenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuResponse>>> GetMenu()
    {
        return Ok(await _menuService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuResponse>> GetMenu(Guid id)
    {
        return Ok(await _menuService.GetAsync(id));
    }

    [HttpPut("{id}")]
    [TypeFilter(typeof(UpdateActionFilter))]
    public async Task<IActionResult> PutMenu(Guid id, UpdateMenu updateMenu)
    {
        await _menuService.UpdateAsync(id,updateMenu);

        return NoContent();
    }
    
    [HttpPut("{id}/close"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutMenuStatusClose(Guid id)
    {
        await _menuService.UpdateStatusCloseAsync(id);
        return NoContent();
    }
    
    [HttpPut("{id}/open"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutMenuStatusOpen(Guid id)
    {
        await _menuService.UpdateStatusOpenAsync(id);
        return NoContent();
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<ActionResult<MenuResponse>> PostMenu(CreateMenu createMenu)
    {
        var menu = await _menuService.CreateAsync(createMenu);
        return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
    }

    [HttpDelete("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteMenu(Guid id)
    {
        await _menuService.DeleteAsync(id);
        
        return NoContent();
    }
}
