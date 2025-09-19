using System.Net;
using System.Net.WebSockets;
using System.Text;

public static class WebSocketServer
{
  public static async Task RunAsync()
  {
    var listener = new HttpListener();
    listener.Prefixes.Add("http://localhost:5000/ws/");
    listener.Start();
    Console.WriteLine("[Server] Listening on ws://localhost:5000/ws/");

    while (true)
    {
      var context = await listener.GetContextAsync();

      if (context.Request.IsWebSocketRequest)
      {
        var wsContext = await context.AcceptWebSocketAsync(null);
        var ws = wsContext.WebSocket;
        Console.WriteLine("[Server] Client connected.");

        Thread.Sleep(2000);

        var buffer = new byte[1024];
        var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
          var msg = Encoding.UTF8.GetString(buffer, 0, result.Count);
          Console.WriteLine($"[Server] Received: {msg}");

          Thread.Sleep(2000);

          var echo = Encoding.UTF8.GetBytes($"Echo: {msg}");
          await ws.SendAsync(echo, WebSocketMessageType.Text, true, CancellationToken.None);

          result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await ws.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        Console.WriteLine("[Server] Connection closed.");
      }
      else
      {
        context.Response.StatusCode = 400;
        context.Response.Close();
      }
    }
  }
}
