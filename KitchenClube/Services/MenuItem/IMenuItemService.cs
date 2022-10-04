namespace KitchenClube.Services;

public interface IMenuitemService
{
    Task<IEnumerable<MenuitemResponse>> GetAllAsync();

    Task<MenuitemResponse> GetAsync(Guid id);

    Task UpdateAsync(Guid id, UpdateMenuitem updateMenuitem);

    Task<MenuitemResponse> CreateAsync(CreateMenuitem createMenuitem); 

    Task DeleteAsync(Guid id);

    Task <IEnumerable<MenuitemResponse>> GetMenuitemsByMenuId(Guid menuId);

    Task <IEnumerable<MenuitemResponse>> GetMenuitemsByFoodId(Guid foodId);
}
