using KitchenClube.Data;
using KitchenClube.Exceptions;
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
            .Select(u => Todto(u)).ToListAsync();
    }

    private static UserMenuItemSelectionResponse Todto(UserMenuItemSelection u)
    {
        return new UserMenuItemSelectionResponse(u.Id, u.MenuitemId, u.UserId, u.Vote);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserMenuItemSelectionResponse>> GetUserMenuItemSelection(Guid id)
    {
        var userMenuItemSelection = await _context.UserMenuItemSelections.FindAsync(id);

        if (userMenuItemSelection == null)
            throw new NotFoundException(nameof(UserMenuItemSelection), id);

        return Todto(userMenuItemSelection);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<UserMenuItemSelectionResponse>>> GetUserMenuItemSelectionsByUserId(Guid userId)
    {
        return await _context.UserMenuItemSelections.Where(u => u.UserId == userId)
            .Select(u => new UserMenuItemSelectionResponse(u.Id, u.MenuitemId, u.UserId, u.Vote)).ToListAsync();
    }

    [HttpGet("menuitem/{menuitemId}")]
    public async Task<ActionResult<IEnumerable<UserMenuItemSelectionResponse>>> GetUserMenuItemSelectionsByMenuitemId(Guid menuitemId)
    {
        return await _context.UserMenuItemSelections.Where(u => u.MenuitemId == menuitemId)
            .Select(u => new UserMenuItemSelectionResponse(u.Id, u.MenuitemId, u.UserId, u.Vote)).ToListAsync();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserMenuItemSelection(Guid id, UpdateUserMenuItemSelection updateUserMenuItemSelection)
    {
        var userMenuItemSelection = _context.UserMenuItemSelections.FirstOrDefault(u => u.Id == id);
        if (userMenuItemSelection is null)
            throw new NotFoundException(nameof(UserMenuItemSelection),id);

        var menuItem = _context.MenuItems.Where(m => m.Id == userMenuItemSelection.MenuitemId).FirstOrDefault();
        if (menuItem.Day < DateTime.Now)
            throw new BadRequestException("Can not change past user selection");

        var user = _context.Users.FirstOrDefault(u => u.Id == updateUserMenuItemSelection.UserId);
        if (user is null)
            throw new NotFoundException(nameof(User),updateUserMenuItemSelection.UserId);

        var menuitem = _context.MenuItems.FirstOrDefault(m => m.Id == updateUserMenuItemSelection.MenuitemId);
        if (menuitem is null)
            throw new NotFoundException(nameof(MenuItem),updateUserMenuItemSelection.MenuitemId);

        userMenuItemSelection.User = user;
        userMenuItemSelection.Menuitem = menuitem;
        userMenuItemSelection.Vote = updateUserMenuItemSelection.Vote;

        _context.UserMenuItemSelections.Update(userMenuItemSelection);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<UserMenuItemSelection>> PostUserMenuItemSelection(CreateUserMenuItemSelection createUserMenuItemSelection)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == createUserMenuItemSelection.UserId);
        if (user == null)
            throw new NotFoundException(nameof(User),createUserMenuItemSelection.UserId);

        var menuItem = _context.MenuItems.FirstOrDefault(m => m.Id == createUserMenuItemSelection.MenuitemId);
        if (menuItem == null)
            throw new NotFoundException(nameof(MenuItem), createUserMenuItemSelection.MenuitemId);

        if (menuItem.Day < DateTime.Now)
            throw new BadRequestException("Can not add user selection because menuitem's day in past");

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
        if (userMenuItemSelection == null)
            throw new NotFoundException(nameof(UserMenuItemSelection),id);

        if (userMenuItemSelection.Menuitem.Day < DateTime.Now)
            throw new BadRequestException("Past user menu selections can not be deleted!");

        _context.UserMenuItemSelections.Remove(userMenuItemSelection);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
