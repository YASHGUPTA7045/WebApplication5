using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Model;
using WebApplication5.Model.DTO;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDBContext _context;
    private readonly IJwtTokenService _jwt;

    public AuthController(AppDBContext context, IJwtTokenService jwt)
    {
        _context = context;
        _jwt = jwt;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _context.users.FirstOrDefaultAsync(x => x.Email == dto.Email && x.Password == dto.Password);
        if (user == null) return Unauthorized("Invalid user");

        var token = _jwt.GenerateToken(user.Email, "User");
        return Ok(new { Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserCreateDto dto)
    {
        var user = new User
        {
            UserName = dto.UserName,
            UserAddress = dto.UserAddress,
            Email = dto.Email,
            Password = dto.Password
        };

        _context.users.Add(user);
        await _context.SaveChangesAsync();
        return Ok("User registered");
    }
}
