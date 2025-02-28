using BowlingSim.Factory;
using BowlingSim.Data;

namespace BowlingSim.Services
{
  internal static class MemberService
  {
    public static void RegisterMember()
    {
      Console.Write("Ange medlemsnamn: ");
      string name = Console.ReadLine() ?? string.Empty;
      if (JsonDb.Instance.Members.Any(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
      {
        Console.WriteLine("Namnet är upptaget, försök igen.");
        return;
      }

      var member = EntityFactory.CreateMember(name);
      JsonDb.Instance.Members.Add(member);
      JsonDb.Instance.Save();

      Console.WriteLine($"Medlem {name} registrerad med ID {member.Id}.");
    }

    public static void ShowMembers()
    {
      foreach (var member in JsonDb.Instance.Members)
      {
        Console.WriteLine($"ID: {member.Id}, Namn: {member.Name}");
      }
    }
  }
}
