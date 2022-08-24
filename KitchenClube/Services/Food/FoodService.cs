namespace KitchenClube.Services;

public class FoodService : ServiceBace<Food>, IFoodService
{
    public FoodService(KitchenClubContext context):base(context,context.Foods) {}

    public async Task<IEnumerable<FoodResponse>> GetAllAsync()
    {
        return await _context.Foods.Select(f => ToDto(f)).ToListAsync();
    }

    public async Task<FoodResponse> GetAsync(Guid id)
    {
        return ToDto(await FindAsync(id));
    }

    public async Task UpdateAsync(Guid id, UpdateFood updateFood)
    {
        var food = await FindAsync(id);

        food.IsActive = updateFood.IsActive;
        food.Name = updateFood.Name;
        food.Description = updateFood.Description;
        food.Image = updateFood.Image;

        _context.Foods.Update(food);
        await _context.SaveChangesAsync();
    }

    public async Task<FoodResponse> CreateAsync(CreateFood createFood)
    {
        var food = new Food
        {
            Name = createFood.Name,
            Description = createFood.Description,
            Image = createFood.Image,
            IsActive = true
        };

        _context.Foods.Add(food);
        await _context.SaveChangesAsync();
        return ToDto(food);
    }

    public async Task DeleteAsync(Guid id)
    {
        var food = await FindAsync(id);

        if (_context.MenuItems.Any(mi => mi.FoodId == id))
            throw new BadRequestException("Food cannot be deleted because it is used on some menu items!");

        _context.Foods.Remove(food);
        await _context.SaveChangesAsync();
    }

    private static FoodResponse ToDto(Food food)
    {
        return new FoodResponse(food.Id, food.Name, food.Image, food.Description, food.IsActive);
    }
}
