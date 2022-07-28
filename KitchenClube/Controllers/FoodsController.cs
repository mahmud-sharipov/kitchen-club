using KitchenClube.Data;

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

    // GET: api/Foods
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Food>>> GetFoods()
    {
        return await _context.Foods.ToListAsync();
    }

    // GET: api/Foods/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Food>> GetFood(Guid id)
    {
        var food = await _context.Foods.FindAsync(id);

        if (food == null) {
            return NotFound();
        }

        return food;
    }

    // PUT: api/Foods/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFood(Guid id, Food food)
    {
        if (id != food.Id) {
            return BadRequest();
        }

        _context.Entry(food).State = EntityState.Modified;

        try {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) {
            if (!FoodExists(id)) {
                return NotFound();
            }
            else {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Foods
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Food>> PostFood(Food food)
    {
        _context.Foods.Add(food);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetFood", new { id = food.Id }, food);
    }

    // DELETE: api/Foods/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFood(Guid id)
    {
        var food = await _context.Foods.FindAsync(id);
        if (food == null) {
            return NotFound();
        }

        _context.Foods.Remove(food);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool FoodExists(Guid id)
    {
        return _context.Foods.Any(e => e.Id == id);
    }
}
