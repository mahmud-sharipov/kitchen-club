namespace KitchenClube.Controllers.V2;

[Route("api/{v:apiVersion}/foods/")]
[ApiController]
[ApiVersion("2.0")]
[Authorize]
public class FoodsController : ControllerBase
{
    private readonly Services.V2.IFoodService _foodService;

    public FoodsController(Services.V2.IFoodService foodService)
    {
        _foodService = foodService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FoodResponse>>> GetFoods() =>
        Ok(await _foodService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<FoodResponse>> GetFood(Guid id) =>
        Ok(await _foodService.GetAsync(id));

    [HttpPut("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutFood(Guid id, UpdateFood updateFood)
    {
        await _foodService.UpdateAsync(id, updateFood);
        return NoContent();
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<ActionResult<FoodResponse>> PostFood(CreateFood createFood)
    {
        var food = await _foodService.CreateAsync(createFood);
        return CreatedAtAction("GetFood", new { id = food.Id }, food);
    }

    [HttpDelete("{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteFood(Guid id)
    {
        await _foodService.DeleteAsync(id);
        return NoContent();
    }
}