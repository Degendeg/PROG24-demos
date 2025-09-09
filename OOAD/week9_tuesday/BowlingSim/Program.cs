using BowlingSim.Factory;
using BowlingSim.Services;

class Program
{
  static void Main()
  {
    EntityFactory.InitializeCounters();

    while (true)
    {
      Console.WriteLine("\nBowlingSim 🎳 \n> Välj ett alternativ:");
      Console.WriteLine("1. Registrera medlem");
      Console.WriteLine("2. Skapa en match");
      Console.WriteLine("3. Bestäm vinnare av en match");
      Console.WriteLine("4. Visa medlemmar");
      Console.WriteLine("5. Visa matcher");
      Console.WriteLine("6. Avsluta");

      string choice = Console.ReadLine() ?? string.Empty;

      switch (choice)
      {
        case "1":
          MemberService.RegisterMember();
          break;
        case "2":
          MatchService.CreateMatch();
          break;
        case "3":
          MatchService.DetermineWinner();
          break;
        case "4":
          MemberService.ShowMembers();
          break;
        case "5":
          MatchService.ShowMatches();
          break;
        case "6":
          return;
        default:
          Console.WriteLine("Ogiltigt val, försök igen.");
          break;
      }
    }
  }
}
