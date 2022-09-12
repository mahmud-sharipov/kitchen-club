namespace KitchenClube.Services.V2;

public interface IMenuItemService
{
    Task<IEnumerable<MenuItemResponse>> GetAllAsync();

    Task<MenuItemResponse> GetAsync(Guid id);

    Task UpdateAsync(Guid id, UpdateMenuItem updateMenuItem);

    Task<MenuItemResponse> CreateAsync(CreateMenuItem createMenuItem); 

    Task DeleteAsync(Guid id);

    Task <IEnumerable<MenuItemResponse>> MenuItemsByMenuId(Guid menuId);

    Task <IEnumerable<MenuItemResponse>> MenuItemsByFoodId(Guid foodId);
}
