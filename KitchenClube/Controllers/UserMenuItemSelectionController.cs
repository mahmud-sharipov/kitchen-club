namespace KitchenClube.Controllers;

[Route("api/{v:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class UserMenuItemSelectionController : ControllerBase
{
    private readonly IUserMenuitemSelectionService _userMenuItemSelection;
    public UserMenuItemSelectionController(IUserMenuitemSelectionService userMenuItemSelection)
    {
        _userMenuItemSelection = userMenuItemSelection;
    }

    [HttpGet]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<UserMenuitemSelectionResponse>>> GetUserMenuItemSelections()
    {
        return Ok(await _userMenuItemSelection.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserMenuitemSelectionResponse>> GetUserMenuItemSelection(Guid id)
    {
        return Ok(await _userMenuItemSelection.GetAsync(id));
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<UserMenuitemSelectionResponse>>> GetUserMenuItemSelectionsByUserId(Guid userId)
    {
        return Ok(await _userMenuItemSelection.UserMenuitemSelectionsByUserId(userId));
    }

    [HttpGet("menuitem/{menuitemId}")]
    public async Task<ActionResult<IEnumerable<UserMenuitemSelectionResponse>>> GetUserMenuItemSelectionsByMenuitemId(Guid menuitemId)
    {
        return Ok(await _userMenuItemSelection.UserMenuitemSelectionsByMenuitemId(menuitemId));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserMenuItemSelection(Guid id, UpdateUserMenuitemSelection updateUserMenuItemSelection)
    {
        await _userMenuItemSelection.UpdateAsync(id, updateUserMenuItemSelection);
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<UserMenuitemSelection>> PostUserMenuItemSelection(CreateUserMenuitemSelection createUserMenuItemSelection)
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
