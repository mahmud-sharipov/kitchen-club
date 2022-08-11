namespace KitchenClube.Services;

public interface IUserService
{
    Task<IEnumerable<UserResponse>> GetAllAsync();
    Task <UserResponse> GetAsync(Guid id);
    Task<UserResponse> CreateAsync(CreateUser createUser);
    Task UpdateAsync(Guid id, UpdateUser updateUser);
    Task DeleteAsync(Guid id);
}
