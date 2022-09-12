namespace KitchenClube.Controllers.V2;

[Route("api/{v:apiVersion}/users/")]
[ApiController]
[ApiVersion("2.0")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly Services.V2.IUserService _userService;

    public UsersController(Services.V2.IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [TypeFilter(typeof(GetAllActionFilter))]
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
    

    [HttpPut("roles/{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutUserRole(Guid id, UpdateUserRole updateUserRole)
    {
        await _userService.UpdateAsync(id, updateUserRole);
        return NoContent();
    }

    [HttpPut("/passwordreset")]
    public async Task<IActionResult> PutUserPassword(ResetPasswordUser reset)
    {
        await _userService.ResetPasswordAsync(reset);
        return NoContent();
    }

    [HttpPost, Authorize(Roles = "Admin")]
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
