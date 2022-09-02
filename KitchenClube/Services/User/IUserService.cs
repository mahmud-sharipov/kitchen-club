namespace KitchenClube.Services;

public interface IUserService
{
    Task<IEnumerable<UserResponse>> GetAllAsync();
    Task<UserResponse> GetAsync(Guid id);
    Task<UserResponse> CreateAsync(CreateUser createUser);
    Task UpdateAsync(Guid id, UpdateUser updateUser);
    Task UpdateAsync(Guid id, UpdateUserRole updateUserRole);
    Task ResetPasswordAsync(ResetPasswordUser reset);
    Task DeleteAsync(Guid id);
}
