using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// HTTP & HTTPS
if (builder.Environment.IsDevelopment())
{
  builder.WebHost.ConfigureKestrel(options =>
  {
    options.ListenAnyIP(5247); // HTTP
    options.ListenAnyIP(7091, listenOptions =>
    {
      listenOptions.UseHttps();
    });
  });
}

// lägg till JWT autentisering
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(
      Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt")["Key"] ?? throw new KeyNotFoundException("Privat nyckel saknas"))
    )
  };

  // JWT skickas i querystring för SignalR
  options.Events = new JwtBearerEvents
  {
    OnMessageReceived = context =>
    {
      var accessToken = context.Request.Query["access_token"];
      var path = context.HttpContext.Request.Path;
      if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chathub"))
      {
        context.Token = accessToken;
      }
      return Task.CompletedTask;
    }
  };
});

builder.Services.AddDbContext<CoolChatDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHub<ChatHub>("/chathub");

app.Run();
