using PatternsDemo.Core;

namespace PatternsDemo.Game
{
  public class BowlingGame : IGame
  {
    public void StartGame(List<string> players) => Console.WriteLine($"The bowling game has started with participants {string.Join(", ", players)} 🎳!");
  }
}