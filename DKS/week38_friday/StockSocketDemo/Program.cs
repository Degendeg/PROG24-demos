using System.Net;
using System.Net.WebSockets;
using System.Text;

class Program
{
  static async Task Main()
  {
    var listener = new HttpListener();
    listener.Prefixes.Add("http://localhost:5000/ws/");
    listener.Start();
    Console.WriteLine("[Server] Listening on ws://localhost:5000/ws/");

    var random = new Random();
    int stockPrice = 100;

    while (listener.IsListening)
    {
      var context = await listener.GetContextAsync();

      if (context.Request.IsWebSocketRequest)
      {
        var wsContext = await context.AcceptWebSocketAsync(null); // handskakning
        var ws = wsContext.WebSocket; // vår förmodade öppna socket

        // simulera aktiekurs
        while (ws.State == WebSocketState.Open)
        {
          stockPrice += random.Next(-3, 4);
          if (stockPrice < 1) stockPrice = 1;

          string message = $"Aktiekurs: {stockPrice} kr";
          byte[] bytes = Encoding.UTF8.GetBytes(message);

          try
          {
            // skicka över socketen
            await ws.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
            Console.WriteLine("[Server] Sent: " + message);
          }
          catch (WebSocketException)
          {
            Console.WriteLine("[Server] Client disconnected");
            break;
          }

          await Task.Delay(1000); // 1 sek mellan uppdateringar
        }
      }
      else
      {
        context.Response.StatusCode = 400; // bad req
        context.Response.Close();
      }
    }
  }
}