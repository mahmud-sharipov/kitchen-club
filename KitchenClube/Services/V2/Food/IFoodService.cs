namespace KitchenClube.Services.V2;
public interface IFoodService
{
    Task <IEnumerable<FoodResponse>> GetAllAsync();

    Task<FoodResponse> GetAsync(Guid id);

    Task<FoodResponse> CreateAsync(CreateFood createFood);

    Task UpdateAsync(Guid id, UpdateFood updateFood);

    Task DeleteAsync(Guid id);
}
