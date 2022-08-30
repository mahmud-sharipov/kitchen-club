﻿namespace KitchenClube.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FoodsController : ControllerBase
{
    private readonly IFoodService _foodService;

    public FoodsController(IFoodService foodService)
    {
        _foodService = foodService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FoodResponse>>> GetFoods() =>
        Ok(await _foodService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<FoodResponse>> GetFood(Guid id) =>
        Ok(await _foodService.GetAsync(id));

    [HttpPut("{id}"), Authorize(Policy = "Admin")]
    public async Task<IActionResult> PutFood(Guid id, UpdateFood updateFood)
    {
        await _foodService.UpdateAsync(id, updateFood);
        return NoContent();
    }

    [HttpPost, Authorize(Policy = "Admin")]
    public async Task<ActionResult<FoodResponse>> PostFood(CreateFood createFood)
    {
        var food = await _foodService.CreateAsync(createFood);
        return CreatedAtAction("GetFood", new { id = food.Id }, food);
    }

    [HttpDelete("{id}"), Authorize(Policy = "Admin")]
    public async Task<IActionResult> DeleteFood(Guid id)
    {
        await _foodService.DeleteAsync(id);
        return NoContent();
    }
}