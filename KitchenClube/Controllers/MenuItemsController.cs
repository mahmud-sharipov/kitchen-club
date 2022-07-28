using KitchenClube.Data;

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

    // GET: api/MenuItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItems()
    {
        return await _context.MenuItems.ToListAsync();
    }

    // GET: api/MenuItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MenuItem>> GetMenuItem(Guid id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);

        if (menuItem == null)
        {
            return NotFound();
        }

        return menuItem;
    }

    // PUT: api/MenuItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMenuItem(Guid id, MenuItem menuItem)
    {
        if (id != menuItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(menuItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MenuItemExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/MenuItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<MenuItem>> PostMenuItem(MenuItem menuItem)
    {
        _context.MenuItems.Add(menuItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMenuItem", new { id = menuItem.Id }, menuItem);
    }

    // DELETE: api/MenuItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItem(Guid id)
    {
        var menuItem = await _context.MenuItems.FindAsync(id);
        if (menuItem == null)
        {
            return NotFound();
        }

        _context.MenuItems.Remove(menuItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MenuItemExists(Guid id)
    {
        return _context.MenuItems.Any(e => e.Id == id);
    }
}
