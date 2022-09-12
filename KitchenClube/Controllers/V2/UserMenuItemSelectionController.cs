namespace KitchenClube.Controllers.V2;

[Route("api/{v:apiVersion}/usermenuitemselection/")]
[ApiController]
[ApiVersion("2.0")]
[Authorize]
public class UserMenuItemSelectionController : ControllerBase
{
    private readonly Services.V2.IUserMenuItemSelectionService _userMenuItemSelection;
    public UserMenuItemSelectionController(Services.V2.IUserMenuItemSelectionService userMenuItemSelection)
    {
        _userMenuItemSelection = userMenuItemSelection;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserMenuItemSelectionResponse>>> GetUserMenuItemSelections()
    {
        return Ok(await _userMenuItemSelection.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserMenuItemSelectionResponse>> GetUserMenuItemSelection(Guid id)
    {
        return Ok(await _userMenuItemSelection.GetAsync(id));
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<UserMenuItemSelectionResponse>>> GetUserMenuItemSelectionsByUserId(Guid userId)
    {
        return Ok(await _userMenuItemSelection.UserMenuItemSelectionsByUserId(userId));
    }

    [HttpGet("menuitem/{menuitemId}")]
    public async Task<ActionResult<IEnumerable<UserMenuItemSelectionResponse>>> GetUserMenuItemSelectionsByMenuitemId(Guid menuitemId)
    {
        return Ok(await _userMenuItemSelection.UserMenuItemSelectionsByMenuitemId(menuitemId));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserMenuItemSelection(Guid id, UpdateUserMenuItemSelection updateUserMenuItemSelection)
    {
        await _userMenuItemSelection.UpdateAsync(id, updateUserMenuItemSelection);
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<UserMenuItemSelection>> PostUserMenuItemSelection(CreateUserMenuItemSelection createUserMenuItemSelection)
    {
        var userMenuItemSelection = await _userMenuItemSelection.CreateAsync(createUserMenuItemSelection);
        return CreatedAtAction("GetUserMenuItemSelection", new { id = userMenuItemSelection.Id }, userMenuItemSelection);
    }

    [HttpDelete("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUserMenuItemSelection(Guid id)
    {
        await _userMenuItemSelection.DeleteAsync(id);
        return NoContent();
    }
}
