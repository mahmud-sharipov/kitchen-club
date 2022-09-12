namespace KitchenClube.Services.V2;

public class RoleService : IRoleService
{
    private readonly UserManager<User> _usermanager;
    private readonly IMapper _mapper;
    private readonly RoleManager<Role> _roleManager;

    public RoleService(UserManager<User> usermanager, IMapper mapper, RoleManager<Role> roleManager)
    {
        _usermanager = usermanager;
        _mapper = mapper;
        _roleManager = roleManager;
    }

    public async Task<ActionResult<IEnumerable<Role>>> GetAllAsync() => await _roleManager.Roles.ToListAsync();

    public async Task<RoleResponse> GetAsync(Guid id)
    {
        return await _roleManager.Roles.Where(r => r.Id == id)
            .Select(r => _mapper.Map<Role,RoleResponse>(r)).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Guid id, UpdateRole updateRole)
    {
        var role = await _roleManager.Roles.Where(r => r.Id == id).FirstOrDefaultAsync();
        _mapper.Map(updateRole, role);

        await _roleManager.UpdateAsync(role);
    }

    public async Task<RoleResponse> CreateAsync(CreateRole createRole)
    {
        var role = new Role(createRole.Name);
        role.IsActive = true;
        await _roleManager.CreateAsync(role);
        return new RoleResponse(role.Id,role.Name,role.IsActive);
    }

    public async Task DeleteAsync(Guid id)
    {
        var role = await _roleManager.Roles.Where(r => r.Id==id).FirstOrDefaultAsync();

        if (role is null)
            throw new NotFoundException("Role", id);

        var urs = await _usermanager.GetUsersInRoleAsync(role.Name);

        if (urs.Count > 0)
            throw new BadRequestException("Cannot delete role because there are Users with it");

        await _roleManager.DeleteAsync(role);
    }
}