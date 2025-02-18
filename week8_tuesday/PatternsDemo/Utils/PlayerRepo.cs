using PatternsDemo.Core;

namespace PatternsDemo.Utils
{
  public class PlayerRepo : IPlayerRepo
  {
    private readonly List<string> _players = [];
    public void AddPlayer(string name) => _players.Add(name);
    public List<string> GetPlayers() => _players;
  }
}