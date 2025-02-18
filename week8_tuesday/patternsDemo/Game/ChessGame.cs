using PatternsDemo.Core;

namespace PatternsDemo.Game
{
  public class ChessGame : IGame
  {
    public void StartGame(List<string> players) => Console.WriteLine($"Chess game was started between {players[0]} and {players[1]} ♟️!");
  }
}