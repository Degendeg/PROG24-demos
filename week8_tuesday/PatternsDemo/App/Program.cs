using DemoApp.Core;
using Microsoft.Extensions.DependencyInjection;
using PatternsDemo.Core;
using PatternsDemo.Utils;

class Program
{
  static void Main()
  {
    // setup DI
    var serviceProvider = new ServiceCollection()
    .AddSingleton<ILogger, ConsoleLogger>()
    .AddSingleton<IPlayerRepo, PlayerRepo>()
    .BuildServiceProvider();

    var logger = serviceProvider.GetService<ILogger>() ??
      throw new InvalidOperationException("Logger is not available.");
    var playerRepo = serviceProvider.GetService<IPlayerRepo>() ??
      throw new InvalidOperationException("PlayerRepo is not available.");

    logger.Log("Adding players...");
    playerRepo.AddPlayer("Alice");
    playerRepo.AddPlayer("Ella");
    playerRepo.AddPlayer("Bob");

    logger.Log("Current players:");
    var players = playerRepo?.GetPlayers() ?? [];
    foreach (var player in players)
    {
      Console.WriteLine(player);
    }

    // factory pattern för att skapa ett spel
    Console.WriteLine("Enter the desired game:");
    var game = GameFactory.CreateGame(Console.ReadLine()) ??
      throw new InvalidOperationException("GameFactory is not available.");
    game?.StartGame(players);
  }
}