namespace KitchenClube.Services;

public class FoodService : ServiceBace<Food>, IFoodService
{
    public FoodService(KitchenClubContext context, IMapper mapper) : base(context, context.Foods, mapper) { }

    public async Task<IEnumerable<FoodResponse>> GetAllAsync()
    {
        return await _context.Foods.Select(f => _mapper.Map<Food, FoodResponse>(f)).ToListAsync();
    }

    public async Task<FoodResponse> GetAsync(Guid id)
    {
        return _mapper.Map<Food, FoodResponse>(await FindOrThrowExceptionAsync(id));
    }

    public async Task UpdateAsync(Guid id, UpdateFood updateFood)
    {
        var food = await FindOrThrowExceptionAsync(id);
        food = _mapper.Map(updateFood, food);
        _context.Foods.Update(food);
        await _context.SaveChangesAsync();
    }

    public async Task<FoodResponse> CreateAsync(CreateFood createFood)
    {
        var food = _mapper.Map<CreateFood, Food>(createFood);
        food.IsActive = true;

        _context.Foods.Add(food);
        await _context.SaveChangesAsync();
        return _mapper.Map<Food, FoodResponse>(food);
    }

    public async Task DeleteAsync(Guid id)
    {
        if (_context.MenuItems.Any(mi => mi.FoodId == id))
            throw new BadRequestException("Food cannot be deleted because it is used on some menu items!");

        var food = await FindOrThrowExceptionAsync(id);
        _context.Foods.Remove(food);
        await _context.SaveChangesAsync();
    }
}
