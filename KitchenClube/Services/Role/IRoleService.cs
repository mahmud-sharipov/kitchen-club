namespace KitchenClube.Services;

public interface IRoleService
{
    Task<IEnumerable<RoleResponse>> GetAllAsync();
    Task<RoleResponse> GetAsync(Guid id);
    Task<RoleResponse> CreateAsync(CreateRole createRole);
    Task UpdateAsync(Guid id, UpdateRole updateRole);
    Task DeleteAsync(Guid id);
}
