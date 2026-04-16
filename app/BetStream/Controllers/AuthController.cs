using BetStream.Domain.Models;
using BetStream.Infrastucture.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;

    public AuthController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User loginOptions)
    {
        // Dummy users (replace with DB)
        var users = new List<User>
        {
            new User { Username = "admin", Password = "123", Role = "Admin" },
            new User { Username = "user", Password = "123", Role = "User" }
        };

        var user = users.FirstOrDefault(u =>
            u.Username == loginOptions.Username &&
            u.Password == loginOptions.Password
        );

        if (user == null)
            return Unauthorized();

        var token = _jwtService.GenerateToken(user);

        return Ok(new { token });
    }
}