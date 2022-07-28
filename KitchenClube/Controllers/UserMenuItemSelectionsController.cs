using KitchenClube.Data;

namespace KitchenClube.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserMenuItemSelectionsController : ControllerBase
{
    private readonly KitchenClubContext _context;

    public UserMenuItemSelectionsController(KitchenClubContext context)
    {
        _context = context;
    }

    // GET: api/UserMenuItemSelections
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserMenuItemSelection>>> GetUserMenuItemSelections()
    {
        return await _context.UserMenuItemSelections.ToListAsync();
    }

    // GET: api/UserMenuItemSelections/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserMenuItemSelection>> GetUserMenuItemSelection(Guid id)
    {
        var userMenuItemSelection = await _context.UserMenuItemSelections.FindAsync(id);

        if (userMenuItemSelection == null)
        {
            return NotFound();
        }

        return userMenuItemSelection;
    }

    // PUT: api/UserMenuItemSelections/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserMenuItemSelection(Guid id, UserMenuItemSelection userMenuItemSelection)
    {
        if (id != userMenuItemSelection.Id)
        {
            return BadRequest();
        }

        _context.Entry(userMenuItemSelection).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserMenuItemSelectionExists(id))
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

    // POST: api/UserMenuItemSelections
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<UserMenuItemSelection>> PostUserMenuItemSelection(UserMenuItemSelection userMenuItemSelection)
    {
        _context.UserMenuItemSelections.Add(userMenuItemSelection);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUserMenuItemSelection", new { id = userMenuItemSelection.Id }, userMenuItemSelection);
    }

    // DELETE: api/UserMenuItemSelections/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserMenuItemSelection(Guid id)
    {
        var userMenuItemSelection = await _context.UserMenuItemSelections.FindAsync(id);
        if (userMenuItemSelection == null)
        {
            return NotFound();
        }

        _context.UserMenuItemSelections.Remove(userMenuItemSelection);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserMenuItemSelectionExists(Guid id)
    {
        return _context.UserMenuItemSelections.Any(e => e.Id == id);
    }
}
