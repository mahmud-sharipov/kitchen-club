namespace KitchenClube.Controllers.V2;

[Route("api/{v:apiVersion}/roles/")]
[ApiController]
[ApiVersion("2.0")]
[Authorize(Roles = "Admin")]
public class RolesController : ControllerBase
{
    private readonly Services.V2.IRoleService _roleService;

    public RolesController(Services.V2.IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        return Ok(await _roleService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RoleResponse>> GetRole(Guid id)
    {
        return Ok(await _roleService.GetAsync(id));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(Guid id, UpdateRole updateRole)
    {
        await _roleService.UpdateAsync(id, updateRole);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(CreateRole createRole)
    {
        var role = await _roleService.CreateAsync(createRole);
        return CreatedAtAction("GetRole", new { id = role.Id }, role);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRole(Guid id)
    {
        await _roleService.DeleteAsync(id);
        return NoContent();
    }
}
