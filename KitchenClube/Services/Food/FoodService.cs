namespace KitchenClube.Services;

public class FoodService : IFoodService
{
    private readonly KitchenClubContext _context;

    public FoodService(KitchenClubContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FoodResponse>> GetAllAsync()
    {
        return await _context.Foods.Select(f => ToDto(f)).ToListAsync();
    }

    public async Task<FoodResponse> GetAsync(Guid id)
    {
        var food = await _context.Foods.FindAsync(id);

        if (food == null)
            throw new NotFoundException(nameof(Food), id);

        return ToDto(food);
    }

    public async Task UpdateAsync(Guid id, UpdateFood updateFood)
    {
        var food = _context.Foods.FirstOrDefault(x => x.Id == id);

        if (food is null)
            throw new NotFoundException(nameof(Food), id);

        food.IsActive = updateFood.IsActive;
        food.Name = updateFood.Name;
        food.Description = updateFood.Description;
        food.Image = updateFood.Image;

        _context.Foods.Update(food);
        await _context.SaveChangesAsync();
    }

    public async Task<FoodResponse> CreateAsync(CreateFood createFood)
    {
        var food = new Food();
        food.Name = createFood.Name;
        food.Description = createFood.Description;
        food.Image = createFood.Image;
        food.IsActive = true;

        _context.Foods.Add(food);
        await _context.SaveChangesAsync();
        return ToDto(food);
    }

    public async Task DeleteAsync(Guid id)
    {
        var food = await _context.Foods.FindAsync(id);
        if (food == null)
            throw new NotFoundException(nameof(Food), id);

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
