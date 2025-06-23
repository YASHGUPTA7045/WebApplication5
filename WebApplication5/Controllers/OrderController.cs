using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Model;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDBContext _context;
        public OrderController(AppDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrder()
        {
            var order = await _context.orders.ToListAsync();
            return Ok(order);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetByid(int id)
        {
            var order = await _context.orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);

        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order xyz)
        {
            var data = new Order
            {
                OrderId = xyz.OrderId,
                OrderName = xyz.OrderName
            };
            _context.orders.Add(data);
            await _context.SaveChangesAsync();
            return Ok(data);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(int id, Order xyz)
        {
            var update = await _context.orders.FindAsync(id);
            if (update == null)
            {
                return NotFound("data not found");
            }
            update.OrderName = xyz.OrderName;
            await _context.SaveChangesAsync();
            return Ok(update);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteorder(int id)
        {
            var delete = await _context.orders.FindAsync(id);
            if (delete == null)
            {
                return NotFound("data not found");
            }
            _context.orders.Remove(delete);
            await _context.SaveChangesAsync();
            return Ok(delete);
        }

    }
}
