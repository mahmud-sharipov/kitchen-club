using KitchenClube.Data;
using KitchenClube.Exceptions;
using KitchenClube.Requests.Food;
using KitchenClube.Responses;

namespace KitchenClube.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FoodsController : ControllerBase
{
    private readonly KitchenClubContext _context;

    public FoodsController(KitchenClubContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FoodResponse>>> GetFoods()
    {
        return await _context.Foods.Select(f => ToDto(f)).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FoodResponse>> GetFood(Guid id)
    {
        var food = await _context.Foods.FindAsync(id);

        if (food == null)
            throw new NotFoundException(nameof(Food), id);

        return ToDto(food);
    }

    private static FoodResponse ToDto(Food food)
    {
        return new FoodResponse(food.Id, food.Name, food.Image, food.Description, food.IsActive);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutFood(Guid id, UpdateFood updateFood)
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

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Food>> PostFood(CreateFood createFood)
    {
        var food = new Food();
        food.Name = createFood.Name;
        food.Description = createFood.Description;
        food.Image = createFood.Image;
        food.IsActive = true;

        _context.Foods.Add(food);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetFood", new { id = food.Id }, food);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFood(Guid id)
    {
        var food = await _context.Foods.FindAsync(id);
        if (food == null)
            throw new NotFoundException("wrong id"); //TODO: change

        if (_context.MenuItems.Any(mi => mi.FoodId == id))
            throw new BadRequestException("Food cannot be deleted because it is used on some menu items!");

        _context.Foods.Remove(food);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
