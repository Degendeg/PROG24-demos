using System.Configuration;
using PatternsDemo.Game;
using PatternsDemo.Core;

namespace DemoApp.Core
{
  public class GameFactory
  {
    public static IGame? CreateGame(string? type)
    {
      if (string.IsNullOrWhiteSpace(type))
      {
        return null;
      }

      string bowlingKey = ConfigurationManager.AppSettings["BowlingGame"] ?? "bowling";
      string chessKey = ConfigurationManager.AppSettings["ChessGame"] ?? "chess";

      return type.ToLowerInvariant() switch
      {
        var t when t == bowlingKey => new BowlingGame(),
        var t when t == chessKey => new ChessGame(),
        _ => throw new ArgumentException("Invalid game type")
      };
    }
  }
}