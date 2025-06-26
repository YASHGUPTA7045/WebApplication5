using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Model;
using WebApplication5.Model.DTO;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDBContext _context;
        public UserController(AppDBContext context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllUser()
        {
            var res = await _context.users.Include(x => x.orders)
                .Select(x => new UserReadDto
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    UserAddress = x.UserAddress,
                    Orders = x.orders.Select(x => new OrderIUserDto
                    {
                        OrderId = x.OrderId,
                        OrderName = x.OrderName
                    }).ToList()
                })
            .ToListAsync();
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDto>> GetUserID(int id)
        {
            var res = await _context.users.Include(x => x.orders)
                .Where(x => x.UserId == id).Select(y => new UserReadDto
                {
                    UserId = y.UserId,
                    UserName = y.UserName,
                    UserAddress = y.UserAddress,
                    Orders = y.orders.Select(x => new OrderIUserDto
                    {
                        OrderId = x.OrderId,
                        OrderName = x.OrderName
                    })
                }).FirstOrDefaultAsync();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<UserCreateDto>> CreateUser(UserCreateDto xyz)
        {
            var create = new User
            {
                UserName = xyz.UserName,
                UserAddress = xyz.UserAddress

            };
            _context.users.Add(create);
            await _context.SaveChangesAsync();
            var data = new UserReadDto
            {
                UserId = create.UserId,
                UserName = create.UserName,
                UserAddress = create.UserAddress

            };
            return Ok(data);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UserUpdateDto>> UpdateUser(int id, UserUpdateDto xyz)
        {
            var user = await _context.users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            user.UserName = xyz.UserName;
            user.UserAddress = xyz.UserAddress;
            await _context.SaveChangesAsync();
            var resul = new UserReadDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserAddress = user.UserAddress

            };
            return Ok(resul);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.users.Remove(user);
            return Ok(user);

        }
    }
}
