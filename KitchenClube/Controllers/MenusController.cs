using KitchenClube.Data;

namespace KitchenClube.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenusController : ControllerBase
{
    private readonly KitchenClubContext _context;

    public MenusController(KitchenClubContext context)
    {
        _context = context;
    }

    // GET: api/Menus
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Menu>>> GetMenu()
    {
        return await _context.Menu.ToListAsync();
    }

    // GET: api/Menus/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Menu>> GetMenu(Guid id)
    {
        var menu = await _context.Menu.FindAsync(id);

        if (menu == null) {
            return NotFound();
        }

        return menu;
    }

    // PUT: api/Menus/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMenu(Guid id, Menu menu)
    {
        if (id != menu.Id) {
            return BadRequest();
        }

        _context.Entry(menu).State = EntityState.Modified;

        try {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) {
            if (!MenuExists(id)) {
                return NotFound();
            }
            else {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Menus
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Menu>> PostMenu(Menu menu)
    {
        _context.Menu.Add(menu);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
    }

    // DELETE: api/Menus/5
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
