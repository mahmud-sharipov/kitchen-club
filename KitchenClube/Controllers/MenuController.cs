using KitchenClube.Data;
using KitchenClube.Exceptions;
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
            .Select(u => ToDto(u)).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuResponse>> GetMenu(Guid id)
    {
        var menu = await _context.Menu.FindAsync(id);

        if (menu == null)
            throw new NotFoundException(nameof(Menu),id);
        return ToDto(menu);
    }

    private static MenuResponse ToDto(Menu menu)
    {
        return new MenuResponse(menu.Id, menu.StartDate, menu.EndDate, menu.Status);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutMenu(Guid id, UpdateMenu updateMenu)
    {
        var menu = _context.Menu.Find(id);
        if (menu is null)
            throw new NotFoundException(nameof(Menu), id);

        if (updateMenu.StartDate > updateMenu.EndDate)
            throw new BadRequestException("End date must be greater than Start date.");

        if (updateMenu.StartDate == updateMenu.EndDate)
            throw new BadRequestException("Start date and End date can not be equal.");

        if (menu.Status is MenuStatus.Closed)
            throw new BadRequestException("Сan not be changed because the menu is closed.");

        if (_context.MenuItems.Any(m => m.MenuId == id))
            throw new BadRequestException("Can not change dates of menu because it is used in menuitems");

        menu.StartDate = updateMenu.StartDate;
        menu.EndDate = updateMenu.EndDate;

        _context.Update(menu);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}/close")]
    public async Task<IActionResult> PutMenuStatusClose(Guid id)
    {
        var menu = await _context.Menu.FindAsync(id);
        if (menu is null)
            throw new NotFoundException(nameof(Menu), id);

        menu.Status = MenuStatus.Closed;
        _context.Update(menu);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id}/open")]
    public async Task<IActionResult> PutMenuStatusOpen(Guid id)
    {
        var menu = await _context.Menu.FindAsync(id);
        if (menu is null)
            throw new NotFoundException(nameof(Menu),id);

        menu.Status = MenuStatus.Active;
        _context.Update(menu);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpPost]
    public async Task<ActionResult<Menu>> PostMenu(CreateMenu createMenu)
    {
        if (createMenu.StartDate == createMenu.EndDate)
            throw new BadRequestException("Start date and End date can not be equal.");

        if (createMenu.EndDate < createMenu.StartDate)
            throw new BadRequestException("End date must be greater than Start date.");

        var menu = new Menu();
        menu.StartDate = createMenu.StartDate;
        menu.EndDate = createMenu.EndDate;
        menu.Status = MenuStatus.Draft;
        _context.Menu.Add(menu);

        await _context.SaveChangesAsync();
        return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenu(Guid id)
    {
        var menu = await _context.Menu.FindAsync(id);
        if (menu == null)
            throw new NotFoundException(nameof(Menu), id);

        if (menu.Status is MenuStatus.Closed)
            throw new BadRequestException("Сan not delete because menu is closed.");

        _context.Menu.Remove(menu);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
