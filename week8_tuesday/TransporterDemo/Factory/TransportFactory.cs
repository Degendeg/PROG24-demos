public static class TransportFactory
{
  public static ITransport CreateTransport(string transportType)
  {
    return transportType.ToLower() switch
    {
      "bil" => new Car(),
      "cykel" => new Bike(),
      "buss" => new Bus(),
      _ => throw new ArgumentException("Ogiltigt transportmedel")
    };
  }
}