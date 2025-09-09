using BowlingSim.Factory;
using BowlingSim.Data;

namespace BowlingSim.Services
{
  internal static class MatchService
  {
    public static void CreateMatch()
    {
      var members = JsonDb.Instance.Members;
      if (members.Count < 2)
      {
        Console.WriteLine("Minst två medlemmar krävs för att skapa en match.");
        return;
      }

      Console.WriteLine("Tillgängliga medlemmar:");
      foreach (var member in members)
      {
        Console.WriteLine($"{member.Id}: {member.Name}");
      }

      int p1Id = ReadIntInput("Ange ID för spelare 1: ");
      int p2Id = ReadIntInput("Ange ID för spelare 2: ");

      if (p1Id == p2Id)
      {
        Console.WriteLine("En spelare kan inte möta sig själv, försök igen!");
        return;
      }

      var player1 = members.Find(m => m.Id == p1Id);
      var player2 = members.Find(m => m.Id == p2Id);

      if (player1 == null || player2 == null)
      {
        Console.WriteLine("Ogiltiga ID.");
        return;
      }

      var match = EntityFactory.CreateMatch(player1, player2);
      JsonDb.Instance.Matches.Add(match);
      JsonDb.Instance.Save();

      Console.WriteLine($"Match skapad mellan {player1.Name} och {player2.Name}.");
    }

    public static void ShowMatches()
    {
      foreach (var match in JsonDb.Instance.Matches)
      {
        Console.WriteLine($"Match ID: {match.Id}, Spelare: {string.Join(", ", match.Player1.Name, match.Player2.Name)}");
      }
    }

    public static void DetermineWinner()
    {
      ShowMatches();

      var matches = JsonDb.Instance.Matches;
      if (matches.Count == 0)
      {
        Console.WriteLine("Inga matcher tillgängliga.");
        return;
      }

      int matchId = ReadIntInput("Ange match-ID: ");

      var match = matches.Find(m => m.Id == matchId);
      if (match == null)
      {
        Console.WriteLine("Match ej funnen.");
        return;
      }

      match.Score1 = new Random().Next(80, 301);
      match.Score2 = new Random().Next(80, 301);

      for (int i = 0; i <= 3; i++)
      {
        Console.Clear();
        Console.Write("Match spelas" + new string('.', i));
        Thread.Sleep(500);
      }

      string winner = match.Score1 > match.Score2 ? match.Player1.Name :
                      match.Score2 > match.Score1 ? match.Player2.Name :
                      "Oavgjort";

      Console.WriteLine($"\nNamn/poäng: {match.Player1.Name} : {match.Score1} | {match.Player2.Name} : {match.Score2}");
      Console.WriteLine($"Vinnare: {winner} 🥇");
      JsonDb.Instance.Save();
    }

    private static int ReadIntInput(string prompt)
    {
      while (true)
      {
        Console.Write(prompt);
        string input = Console.ReadLine()!;
        if (int.TryParse(input, out int result))
        {
          return result;
        }
        Console.WriteLine("Felaktig inmatning, ange ett giltigt heltal.");
      }
    }
  }
}
