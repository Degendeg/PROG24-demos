using System.Net.WebSockets;
using System.Text;

public static class WebSocketClient
{
  public static async Task RunAsync()
  {
    using var ws = new ClientWebSocket();
    await ws.ConnectAsync(new Uri("ws://localhost:5000/ws/"), CancellationToken.None);

    Console.WriteLine("[Client] Connected, sending message...");

    var msgBytes = Encoding.UTF8.GetBytes("Hello Server!");
    await ws.SendAsync(msgBytes, WebSocketMessageType.Text, true, CancellationToken.None);

    var buffer = new byte[1024];
    var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
    Console.WriteLine($"[Client] Received from server: {Encoding.UTF8.GetString(buffer, 0, result.Count)}");

    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Demo finished", CancellationToken.None);
  }
}
