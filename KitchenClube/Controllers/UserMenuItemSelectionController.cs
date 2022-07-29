using KitchenClube.Data;
using KitchenClube.Requests.UserMenuItemSelection;
using KitchenClube.Responses;

namespace KitchenClube.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserMenuItemSelectionController : ControllerBase
{
    private readonly KitchenClubContext _context;

    public UserMenuItemSelectionController(KitchenClubContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserMenuItemSelectionResponse>>> GetUserMenuItemSelections()
    {
        return await _context.UserMenuItemSelections
            .Select(u => new UserMenuItemSelectionResponse(u.Id, u.MenuitemId, u.UserId, u.Vote)).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserMenuItemSelectionResponse>> GetUserMenuItemSelection(Guid id)
    {
        var userMenuItemSelection = await _context.UserMenuItemSelections.FindAsync(id);

        if (userMenuItemSelection == null) {
            return NotFound();
        }

        return
            new UserMenuItemSelectionResponse(userMenuItemSelection.Id,
            userMenuItemSelection.MenuitemId, userMenuItemSelection.UserId, userMenuItemSelection.Vote);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserMenuItemSelection(Guid id, UpdateUserMenuItemSelection updateUserMenuItemSelection)
    {
        //TODO: Do not allow to change if menu item day in past.

        var userMenuItemSelection = _context.UserMenuItemSelections.FirstOrDefault(u => u.Id == id);
        if (userMenuItemSelection is null)
            return NotFound(id);

        var user = _context.Users.FirstOrDefault(u => u.Id == updateUserMenuItemSelection.UserId);
        if (user is null) {
            return NotFound(updateUserMenuItemSelection.UserId);
        }

        var menuitem = _context.MenuItems.FirstOrDefault(m => m.Id == updateUserMenuItemSelection.MenuitemId);
        if (menuitem is null)
            return NotFound(updateUserMenuItemSelection.MenuitemId);

        userMenuItemSelection.User = user;
        userMenuItemSelection.Menuitem = menuitem;
        userMenuItemSelection.Vote = updateUserMenuItemSelection.Vote;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<UserMenuItemSelection>> PostUserMenuItemSelection(CreateUserMenuItemSelection createUserMenuItemSelection)
    {
        //TODO: Do not allow to add if menu item day in past.

        var user = _context.Users.FirstOrDefault(u => u.Id == createUserMenuItemSelection.UserId);
        if (user == null)
            return NotFound(createUserMenuItemSelection.UserId);

        var menuItem = _context.MenuItems.FirstOrDefault(m => m.Id == createUserMenuItemSelection.MenuitemId);
        if (menuItem == null)
            return NotFound(createUserMenuItemSelection.MenuitemId);

        var userMenuItemSelection = new UserMenuItemSelection();
        userMenuItemSelection.User = user;
        userMenuItemSelection.Menuitem = menuItem;
        userMenuItemSelection.Vote = createUserMenuItemSelection.Vote;

        _context.UserMenuItemSelections.Add(userMenuItemSelection);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUserMenuItemSelection", new { id = userMenuItemSelection.Id }, userMenuItemSelection);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserMenuItemSelection(Guid id)
    {
        var userMenuItemSelection = await _context.UserMenuItemSelections.FindAsync(id);
        if (userMenuItemSelection == null) {
            return NotFound();
        }
        //TODO: Do not allow to delete if menu item day in past.

        _context.UserMenuItemSelections.Remove(userMenuItemSelection);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserMenuItemSelectionExists(Guid id)
    {
        return _context.UserMenuItemSelections.Any(e => e.Id == id);
    }
}
