namespace KitchenClube.Services;

public interface IUserMenuitemSelectionService
{
    Task< IEnumerable<UserMenuitemSelectionResponse>> GetAllAsync();

    Task<UserMenuitemSelectionResponse> GetAsync(Guid id);

    Task<UserMenuitemSelectionResponse> CreateAsync(CreateUserMenuitemSelection CreateUserMenuitemSelection);

    Task UpdateAsync(Guid id,UpdateUserMenuitemSelection UpdateUserMenuitemSelection);

    Task DeleteAsync(Guid id);

    Task <IEnumerable<UserMenuitemSelectionResponse>> UserMenuitemSelectionsByUserId(Guid userId);

    Task <IEnumerable<UserMenuitemSelectionResponse>> UserMenuitemSelectionsByMenuitemId(Guid menuitemId);
}
