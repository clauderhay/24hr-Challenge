using Microsoft.AspNetCore.Mvc;
using _24hr_Code_Challenge.Models;
using _24hr_Code_Challenge.Data;
using Microsoft.EntityFrameworkCore;

namespace _24hr_Code_Challenge.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly PizzaPlaceDbContext _context;

        public OrderController(PizzaPlaceDbContext context)
        {
            _context = context;
        }

        // GET: api/order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders!.ToListAsync();
        }

        // GET: api/order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders!.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // POST: api/order
        [HttpPost("insert/")]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders!.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }

        // PUT: api/order/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // DELETE: api/order/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders!.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders!.Any(e => e.OrderId == id);
        }

        // GET: api/order/orderdetails
        [HttpGet("orderdetails")]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
        {
            // Include the related Order and Pizza entities
            var orderDetails = await _context.OrderDetails!
                .Include(od => od.Order)
                .Include(od => od.Pizza)
                .ToListAsync();

            return Ok(orderDetails);
        }

        // POST: api/order/orderdetails
        [HttpPost("orderdetails/insert")]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails!.Add(orderDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderDetail), new { id = orderDetail.OrderDetailId }, orderDetail);
        }

        // GET: api/order/orderdetails/5
        [HttpGet("orderdetails/{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails!.FindAsync(id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            return orderDetail;
        }

        // DELETE: api/order/orderdetails/5
        [HttpDelete("orderdetails/delete/{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails!.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("total-revenue")]
        public ActionResult<object> GetTotalRevenue()
        {
            var totalRevenue = _context.OrderDetails!
                .Sum(od => od.Quantity * od.Pizza!.Price);

            return Ok(new { TotalRevenue = totalRevenue });
        }
    }
}
