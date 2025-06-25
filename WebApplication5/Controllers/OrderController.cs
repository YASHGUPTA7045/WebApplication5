using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Model;
using WebApplication5.Model.DTO;

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
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAllOrder()
        {
            var order = await _context.orders.Include(x => x.user)
                .Select(x => new OrderReadDto
                {
                    OrderName = x.OrderName,
                    OrderId = x.OrderId,
                    UserId = x.UserId,
                    UserName = x.user.UserName,
                    UserAddress = x.user.UserAddress,
                })
              .ToListAsync();
            return Ok(order);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReadDto>> GetByid(int id)
        {
            var order = await _context.orders.FirstOrDefaultAsync(x => x.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            var s = new OrderReadDto
            {
                OrderId = order.OrderId,
                OrderName = order.OrderName

            };
            return Ok(s);

        }
        [HttpPost]
        public async Task<ActionResult<OrderCreateDto>> CreateOrder(OrderCreateDto xyz)
        {
            var data = new Order
            {

                OrderName = xyz.OrderName
            };
            await _context.orders.AddAsync(data);
            await _context.SaveChangesAsync();
            var dto = new OrderReadDto
            {
                OrderId = data.OrderId,
                OrderName = data.OrderName
            };
            return Ok(dto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderUpdateDto>> UpdateOrder(int id, OrderUpdateDto xyz)
        {
            var update = await _context.orders.FirstOrDefaultAsync(x => x.OrderId == id);
            if (update == null)
            {
                return NotFound("data not found");
            }
            update.OrderName = xyz.OrderName;
            await _context.SaveChangesAsync();
            var Out = new OrderUpdateDto
            {
                OrderName = update.OrderName
            };
            return Ok(Out);

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
