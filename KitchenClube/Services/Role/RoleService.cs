﻿namespace KitchenClube.Services;

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

    public async Task<IEnumerable<RoleResponse>> GetAllAsync()
    {
        return await _roleManager.Roles.Select(r => _mapper.Map<Role, RoleResponse>(r)).ToListAsync();
    }

    public async Task<RoleResponse> GetAsync(Guid id)
    {
        var role = await _roleManager.Roles.Where(r => r.Id == id).FirstOrDefaultAsync();

        if (role is null)
            throw new NotFoundException(nameof(Role), id);

        return _mapper.Map<Role, RoleResponse>(role);
    }

    public async Task UpdateAsync(Guid id, UpdateRole updateRole)
    {
        var role = await _roleManager.Roles.Where(r => r.Id == id).FirstOrDefaultAsync();

        if (role is null)
            throw new NotFoundException(nameof(Role), id);

        _mapper.Map(updateRole, role);

        await _roleManager.UpdateAsync(role);
    }

    public async Task<RoleResponse> CreateAsync(CreateRole createRole)
    {
        var role = new Role(createRole.Name);
        role.IsActive = true;
        await _roleManager.CreateAsync(role);
        return new RoleResponse(role.Id, role.Name, role.IsActive);
    }

    public async Task DeleteAsync(Guid id)
    {
        var role = await _roleManager.Roles.Where(r => r.Id == id).FirstOrDefaultAsync();

        if (role is null)
            throw new NotFoundException("Role", id);

        var urs = await _usermanager.GetUsersInRoleAsync(role.Name);

        if (urs.Count > 0)
            throw new BadRequestException("Cannot delete role because there are some Users with it");

        await _roleManager.DeleteAsync(role);
    }
}