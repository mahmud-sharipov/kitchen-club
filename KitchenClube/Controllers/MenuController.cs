using KitchenClube.Data;
using KitchenClube.Requests.Menu;
using KitchenClube.Responses;

namespace KitchenClube.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly KitchenClubContext _context;

    public MenuController(KitchenClubContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuResponse>>> GetMenu()
    {
        return await _context.Menu
            .Select(u=>new MenuResponse(u.Id,u.StartDate,u.EndDate,u.Status)).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuResponse>> GetMenu(Guid id)
    {
        var menu = await _context.Menu.FindAsync(id);

        if (menu == null) 
            return NotFound();
        
        return new MenuResponse(menu.Id, menu.StartDate, menu.EndDate, menu.Status);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMenu(Guid id, UpdateMenu updateMenu)
    {
        var menu = _context.Menu.Find(id);
        if (menu is null)
            return NotFound();

        if (updateMenu.StartDate > updateMenu.EndDate || updateMenu.StartDate == updateMenu.EndDate) 
            throw new Exception("Wrong Date");        

        menu.StartDate = updateMenu.StartDate;
        menu.EndDate = updateMenu.EndDate;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Menu>> PostMenu(CreateMenu createMenu)
    {
        if (createMenu.StartDate == createMenu.EndDate || createMenu.EndDate < createMenu.StartDate) {
            throw new Exception("Wrong dates");
        }

        var menu = new Menu();
        menu.StartDate = createMenu.StartDate;
        menu.EndDate = createMenu.EndDate;
        menu.Status = createMenu.Status;
        _context.Menu.Add(menu);

        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenu(Guid id)
    {
        var menu = await _context.Menu.FindAsync(id);
        if (menu == null) {
            return NotFound();
        }

        _context.Menu.Remove(menu);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MenuExists(Guid id)
    {
        return _context.Menu.Any(e => e.Id == id);
    }
}
