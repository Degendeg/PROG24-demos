class Program
{
  static async Task Main()
  {
    Console.WriteLine("IP Scanner Demo");

    Console.Write("Start IP: ");
    string startIp = Console.ReadLine() ?? "";

    Console.Write("End IP: ");
    string endIp = Console.ReadLine() ?? "";

    var scanner = new IpScanner();

    var activeIps = await scanner.ScanAsync(startIp, endIp);

    Console.WriteLine("\nActive IPs:");
    foreach (var ip in activeIps)
    {
      Console.WriteLine(ip);
    }

    Console.WriteLine("\nDone! Press any key to exit.");
    Console.ReadKey();
  }
}
