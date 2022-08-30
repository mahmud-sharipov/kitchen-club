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

    [HttpGet, Authorize(Policy = "All")]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
    {
        return Ok(await _userService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetUser(Guid id)
    {
        return Ok(await _userService.GetAsync(id));
    }

    [HttpGet("roles/{id}"), Authorize(Policy = "Admin")]
    public async Task<ActionResult<UserResponse>> GetUserByRole(Guid id)
    {
        return Ok(await _userService.GetByRoleAsync(id));
    }

    [HttpPut("{id}"), Authorize(Policy = "All")]
    public async Task<IActionResult> PutUser(Guid id, UpdateUser updateUser)
    {
        await _userService.UpdateAsync(id, updateUser);
        return NoContent();
    }

    [HttpPost, Authorize(Policy = "Admin")]
    public async Task<ActionResult<UserResponse>> PostUser(CreateUser createUser)
    {
        var user = await _userService.CreateAsync(createUser);
        return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }

    [HttpDelete("{id}"), Authorize(Policy = "Admin")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }
}
