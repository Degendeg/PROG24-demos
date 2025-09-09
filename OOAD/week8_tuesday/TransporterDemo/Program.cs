class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Välj transportmedel: bil, cykel, buss");
    string choice = Console.ReadLine() ?? string.Empty;

    // Skapa rätt transportobjekt baserat på användarens val
    ITransport transport = TransportFactory.CreateTransport(choice);

    // Hämta och visa restiden
    transport.GetTravelTime();
  }
}