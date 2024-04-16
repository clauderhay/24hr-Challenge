using Microsoft.AspNetCore.Mvc;
using _24hr_Code_Challenge.Models;
using _24hr_Code_Challenge.Data;
using Microsoft.EntityFrameworkCore;

namespace _24hr_Code_Challenge.Controllers
{
    [ApiController]
    [Route("api/pizza")]
    public class PizzaController : ControllerBase
    {
        private readonly PizzaPlaceDbContext _context;

        public PizzaController(PizzaPlaceDbContext context)
        {
            _context = context;
        }

        [HttpGet("test")]
        public async Task<IActionResult> TestDatabaseConnection()
        {
            try
            {
                // Test database connection by querying for some data
                var pizzas = await _context.Pizzas!.ToListAsync();
                return Ok("Successfully connected to the database!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to connect to the database: {ex.Message}");
            }
        }

        // GET: api/pizza
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pizza>>> GetPizzas()
        {
            var pizza = await _context.Pizzas!
                .Include(p => p.PizzaType)
                .ToListAsync();

            return Ok(pizza);
        }

        // GET: api/pizza/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> GetPizza(string id)
        {
            var pizza = await _context.Pizzas!.FindAsync(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return pizza;
        }

        // POST: api/pizza
        [HttpPost]
        public async Task<ActionResult<Pizza>> PostPizza(Pizza pizza)
        {
            _context.Pizzas!.Add(pizza);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPizza), new { id = pizza.PizzaId }, pizza);
        }

        // PUT: api/pizza/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizza(string id, Pizza pizza)
        {
            if (id != pizza.PizzaId)
            {
                return BadRequest();
            }

            _context.Entry(pizza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/pizza/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizza(string id)
        {
            var pizza = await _context.Pizzas!.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }

            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PizzaExists(string id)
        {
            return _context.Pizzas!.Any(e => e.PizzaId == id);
        }

        // GET: api/pizza/pizzatypes
        [HttpGet("pizzatypes")]
        public async Task<ActionResult<IEnumerable<PizzaType>>> GetPizzaTypes()
        {
            return await _context.PizzaTypes!.ToListAsync();
        }

        // POST: api/pizza/pizzatypes
        [HttpPost("pizzatypes")]
        public async Task<ActionResult<PizzaType>> PostPizzaType(PizzaType pizzaType)
        {
            _context.PizzaTypes!.Add(pizzaType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPizzaType), new { id = pizzaType.PizzaTypeId }, pizzaType);
        }

        // GET: api/pizza/pizzatypes/5
        [HttpGet("pizzatypes/{id}")]
        public async Task<ActionResult<PizzaType>> GetPizzaType(string id)
        {
            var pizzaType = await _context.PizzaTypes!.FindAsync(id);

            if (pizzaType == null)
            {
                return NotFound();
            }

            return pizzaType;
        }

        // DELETE: api/pizza/pizzatypes/5
        [HttpDelete("pizzatypes/{id}")]
        public async Task<IActionResult> DeletePizzaType(string id)
        {
            var pizzaType = await _context.PizzaTypes!.FindAsync(id);
            if (pizzaType == null)
            {
                return NotFound();
            }

            _context.PizzaTypes.Remove(pizzaType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("popular-pizza-types")]
        public ActionResult<IEnumerable<object>> GetPopularPizzaTypes()
        {
            var popularPizzaTypes = _context.OrderDetails!
                .GroupBy(od => od.PizzaId)
                .Select(g => new
                {
                    PizzaType = g.First().Pizza!.PizzaType!.Name,
                    TotalOrders = g.Count()
                })
                .OrderByDescending(x => x.TotalOrders)
                .Take(3) // Retrieve top 3 popular pizza types
                .ToList();

            return Ok(popularPizzaTypes);
        }
    }
}
