using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
  private readonly IConfiguration _conf;

  public record LoginRequest(string Username, string Password);

  // fake users
  private readonly Dictionary<string, string> _users = new()
  {
    { "demouser", "12345"},
    { "admin", "password"},
  };

  public AuthController(IConfiguration conf)
  {
    _conf = conf;
  }
  [HttpPost("login")]
  public IActionResult Login([FromBody] LoginRequest request)
  {
    if (!_users.TryGetValue(request.Username, out var pwd) || pwd != request.Password)
    {
      return Unauthorized("Ogiltigt användarnamn eller lösenord");
    }

    var claims = new[]
    {
      new Claim(ClaimTypes.Name, "demoUser"),
      new Claim(ClaimTypes.Role, "Admin"),
    };
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf.GetSection("Jwt")["Key"] ?? throw new KeyNotFoundException("Privat nyckel saknas")));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      issuer: "demo",
      audience: "demo",
      claims: claims,
      expires: DateTime.Now.AddHours(1),
      signingCredentials: credentials
    );

    var jwt = new JwtSecurityTokenHandler().WriteToken(token);
    return Ok(new { token = jwt });
  }
}