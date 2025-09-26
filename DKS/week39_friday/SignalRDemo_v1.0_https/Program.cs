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

builder.Services.AddSignalR();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.MapHub<ChatHub>("/chathub");

app.Run();
