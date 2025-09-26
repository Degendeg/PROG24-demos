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

  public AuthController(IConfiguration conf)
  {
    _conf = conf;
  }
  [HttpGet("token")]
  public IActionResult GetToken()
  {
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

    Console.WriteLine($"Token: {jwt}");

    return Ok(new { token = jwt });

  }
}