namespace KitchenClube.Services;

public class RoleService : ServiceBace<Role>, IRoleService
{
    public RoleService(KitchenClubContext context, IMapper mapper) : base(context,context.Roles, mapper) {}
    public async Task<IEnumerable<RoleResponse>> GetAllAsync()
    {
        return await _context.Roles.Select(u=>_mapper.Map<Role, RoleResponse>(u)).ToListAsync();
    }

    public async Task<RoleResponse> GetAsync(Guid id)
    {
        return _mapper.Map<Role, RoleResponse>(await FindOrThrowExceptionAsync(id));
    }

    public async Task UpdateAsync(Guid id, UpdateRole updateRole)
    {
        var role = await FindOrThrowExceptionAsync(id);
        role = _mapper.Map(updateRole,role);

        _context.Roles.Update(role);
        await _context.SaveChangesAsync();
    }

    public async Task<RoleResponse> CreateAsync(CreateRole createRole)
    {
        var role = _mapper.Map<CreateRole, Role>(createRole);
        role.IsActive = true;
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return _mapper.Map<Role, RoleResponse>(role);
    }

    public async Task DeleteAsync(Guid id)
    {
        var role = await FindOrThrowExceptionAsync(id);

        if (_context.Users.Any(u => u.RoleId == role.Id))
            throw new BadRequestException("Cannot delete role because there are Users with it");

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
    }
}