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
  private readonly CoolChatDbContext _db;

  public record LoginRequest(string Username, string Password);
  public record RegisterRequest(string Username, string Password);

  public AuthController(IConfiguration conf, CoolChatDbContext db)
  {
    _conf = conf;
    _db = db;
  }

  [HttpPost("register")]
  public IActionResult Register([FromBody] RegisterRequest request)
  {
    if (_db.Users.Any(u => u.Username == request.Username))
    {
      return BadRequest("Användarnamn upptaget");
    }

    var user = new User
    {
      Username = request.Username,
      PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
    };
    _db.Users.Add(user);
    _db.SaveChanges();

    return Ok("Registrerad");
  }

  [HttpPost("login")]
  public IActionResult Login([FromBody] LoginRequest request)
  {
    var user = _db.Users.FirstOrDefault(u => u.Username == request.Username);
    if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
    {
      return Unauthorized("Ogiltigt användarnamn eller lösenord");
    }

    var claims = new[]
    {
      new Claim(ClaimTypes.Name, user.Username),
      new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    };
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf.GetSection("Jwt")["Key"] ?? throw new KeyNotFoundException("Privat nyckel saknas")));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      claims: claims,
      expires: DateTime.Now.AddHours(1),
      signingCredentials: credentials
    );

    var jwt = new JwtSecurityTokenHandler().WriteToken(token);
    return Ok(new { token = jwt });
  }
}