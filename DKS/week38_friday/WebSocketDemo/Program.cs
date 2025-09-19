class Program
{
  static async Task Main()
  {
    Console.WriteLine("-- WebSocket Demo (Console) --");

    // Starta servern parallellt
    _ = Task.Run(() => WebSocketServer.RunAsync());

    // funkar ej, servern startar men klienten får aldrig köra, därav discard
    // await Task.Run(WebSocketServer.RunAsync);

    // Vänta lite så servern hinner starta
    await Task.Delay(1000);

    // Starta klient
    await WebSocketClient.RunAsync();

    Console.WriteLine("Tryck på valfri tangent för att avsluta...");
    Console.ReadKey();
  }
}