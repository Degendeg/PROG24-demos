namespace PatternsDemo.Core
{
  public interface IPlayerRepo
  {
    void AddPlayer(string name);
    List<string> GetPlayers();
  }
}