using BowlingSim.Models;
using BowlingSim.Data;

namespace BowlingSim.Factory
{
  internal static class EntityFactory
  {
    private static int memberIdCounter;
    private static int matchIdCounter;

    public static void InitializeCounters()
    {
      memberIdCounter = JsonDb.Instance.Members.Max(m => m.Id) + 1;
      matchIdCounter = JsonDb.Instance.Matches.Max(m => m.Id) + 1;
    }

    public static Member CreateMember(string name)
    {
      return new Member(memberIdCounter++, name);
    }

    public static Match CreateMatch(Member player1, Member player2)
    {
      return new Match(matchIdCounter++, player1, player2);
    }
  }
}
