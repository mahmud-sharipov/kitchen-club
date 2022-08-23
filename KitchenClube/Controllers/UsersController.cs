namespace KitchenClube.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
    {
        return Ok(await _userService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetUser(Guid id)
    {
        return Ok(await _userService.GetAsync(id));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(Guid id, UpdateUser updateUser)
    {
        await _userService.UpdateAsync(id, updateUser);
        return NoContent();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UserResponse>> PostUser(CreateUser createUser)
    {
        var user = await _userService.CreateAsync(createUser);
        return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }

    [HttpDelete("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }
}
