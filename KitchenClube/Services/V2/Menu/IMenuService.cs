namespace KitchenClube.Services.V2;
public interface IMenuService
{
    Task <IEnumerable<MenuResponse>> GetAllAsync ();

    Task<MenuResponse> GetAsync(Guid id);

    Task<MenuResponse> CreateAsync(CreateMenu createMenu);

    Task UpdateAsync(Guid id,UpdateMenu updateMenu);

    Task DeleteAsync(Guid id);
    Task UpdateStatusCloseAsync(Guid id);
    Task UpdateStatusOpenAsync(Guid id);
}
