using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseWebSockets();

app.Map("/ws", async context =>
{
  if (context.WebSockets.IsWebSocketRequest)
  {
    using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
    Console.WriteLine($"[Server] Client connected");

    var buffer = new byte[1024];

    try
    {
      while (webSocket.State == WebSocketState.Open)
      {
        // skicka meddelande varje sekund
        var msg = $"Server time: {DateTime.Now}";
        var bytes = Encoding.UTF8.GetBytes(msg);
        await webSocket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);

        if (webSocket.State == WebSocketState.Open)
        {
          if (webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), context.RequestAborted).IsCompleted)
          {
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), context.RequestAborted);
            if (result.MessageType == WebSocketMessageType.Close)
            {
              await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", context.RequestAborted);
            }
          }
        }
        await Task.Delay(1000);
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"[Server] Connection closed unexpectedly. Message: {ex.Message}");
    }
    Console.WriteLine("[Server] Client disconnected.");
  }
  else
  {
    context.Response.StatusCode = 400; // bad req
  }
});

app.Run();
