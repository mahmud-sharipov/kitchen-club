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
            throw new NotFoundException("wrong id");
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
            throw new NotFoundException("wrong id");

        if (updateMenu.StartDate > updateMenu.EndDate || updateMenu.StartDate == updateMenu.EndDate)
            throw new BadRequestException("Wrong Date");

        if (menu.Status is MenuStatus.Closed) 
            throw new BadRequestException("Cant change closed menu");
        

        menu.StartDate = updateMenu.StartDate;
        menu.EndDate = updateMenu.EndDate;
        menu.Status = updateMenu.Status;

        _context.Update(menu);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Menu>> PostMenu(CreateMenu createMenu)
    {
        if (createMenu.StartDate == createMenu.EndDate || createMenu.EndDate < createMenu.StartDate) 
            throw new BadRequestException("Wrong dates");        

        var menu = new Menu();
        menu.StartDate = createMenu.StartDate;
        menu.EndDate = createMenu.EndDate;
        menu.Status = MenuStatus.Active;
        _context.Menu.Add(menu);

        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenu(Guid id)
    {
        var menu = await _context.Menu.FindAsync(id);
        if (menu == null)
            throw new NotFoundException("wrong id");

        if (menu.Status is MenuStatus.Closed) 
            throw new BadRequestException("Cant delete closed menu");
        

        _context.Menu.Remove(menu);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
