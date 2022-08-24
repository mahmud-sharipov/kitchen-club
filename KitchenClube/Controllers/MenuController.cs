namespace KitchenClube.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;
    public MenuController(IMenuService menuService)
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

    private static MenuResponse ToDto(Menu menu)
    {
        return new MenuResponse(menu.Id, menu.StartDate, menu.EndDate, menu.Status);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMenu(Guid id, UpdateMenu updateMenu)
    {
        await _menuService.UpdateAsync(id,updateMenu);

        return NoContent();
    }

    [HttpPut("{id}/close")]
    public async Task<IActionResult> PutMenuStatusClose(Guid id)
    {
        await _menuService.UpdateStatusCloseAsync(id);
        return NoContent();
    }

    [HttpPut("{id}/open")]
    public async Task<IActionResult> PutMenuStatusOpen(Guid id)
    {
        await _menuService.UpdateStatusOpenAsync(id);
        return NoContent();
    }
    
    [HttpPost]
    public async Task<ActionResult<MenuResponse>> PostMenu(CreateMenu createMenu)
    {
        var menu = await _menuService.CreateAsync(createMenu);
        return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenu(Guid id)
    {
        await _menuService.DeleteAsync(id);
        
        return NoContent();
    }
}
