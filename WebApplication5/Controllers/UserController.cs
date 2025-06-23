using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Model;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser()
        {
            var res = await _context.users.ToListAsync();
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserID(int id)
        {
            var res = await _context.users.FindAsync(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User xyz)
        {
            var create = new User
            {
                UserName = xyz.UserName,
                UserAddress = xyz.UserAddress

            };
            _context.users.Add(create);
            await _context.SaveChangesAsync();
            return Ok(create);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User xyz)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.UserName = xyz.UserName;
            user.UserAddress = xyz.UserAddress;
            await _context.SaveChangesAsync();
            return Ok(user);
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
