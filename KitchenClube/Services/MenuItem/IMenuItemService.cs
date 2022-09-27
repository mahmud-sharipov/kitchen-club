namespace KitchenClube.Services;

public interface IMenuItemService
{
    Task<IEnumerable<MenuItemResponse>> GetAllAsync();

    Task<MenuItemResponse> GetAsync(Guid id);

    Task UpdateAsync(Guid id, UpdateMenuItem updateMenuItem);

    Task<MenuItemResponse> CreateAsync(CreateMenuItem createMenuItem); 

    Task DeleteAsync(Guid id);

    Task <IEnumerable<MenuItemResponse>> GetMenuItemsByMenuId(Guid menuId);

    Task <IEnumerable<MenuItemResponse>> GetMenuItemsByFoodId(Guid foodId);
}
