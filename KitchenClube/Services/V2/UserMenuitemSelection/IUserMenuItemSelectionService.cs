namespace KitchenClube.Services.V2;

public interface IUserMenuItemSelectionService
{
    Task< IEnumerable<UserMenuItemSelectionResponse>> GetAllAsync();

    Task<UserMenuItemSelectionResponse> GetAsync(Guid id);

    Task<UserMenuItemSelectionResponse> CreateAsync(CreateUserMenuItemSelection CreateUserMenuItemSelection);

    Task UpdateAsync(Guid id,UpdateUserMenuItemSelection UpdateUserMenuItemSelection);

    Task DeleteAsync(Guid id);

    Task <IEnumerable<UserMenuItemSelectionResponse>> UserMenuItemSelectionsByUserId(Guid userId);

    Task <IEnumerable<UserMenuItemSelectionResponse>> UserMenuItemSelectionsByMenuitemId(Guid menuitemId);
}
