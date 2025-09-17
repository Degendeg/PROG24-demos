using System.Net;
using System.Net.NetworkInformation;

class Program
{
  static async Task Main()
  {
    // Hämta vår publika IP adress via extern tjänst
    using var client = new HttpClient();
    string publicIp = await client.GetStringAsync("https://api.ipify.org/");
    Console.WriteLine($"Public ip: {publicIp}");

    // Hämta den lokala privata IPn
    string localIp = Dns.GetHostAddresses(Dns.GetHostName())
                      .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)?.ToString() ?? "Not found";
    Console.WriteLine($"Local ip: {localIp}");

    // kontrollera privat eller publik IP
    var testIps = new[]
    {
      IPAddress.Parse("172.20.203.181"),
      IPAddress.Parse("185.106.180.53")
    };
    foreach (var ip in testIps)
    {
      bool isPrivate = IsPrivateIp(ip);
      Console.WriteLine($"{ip} is {(isPrivate ? "Private" : "Public")}");
    }

    // Visa subnätmask för det aktiva nätverket
    var nic = NetworkInterface.GetAllNetworkInterfaces()
                              .FirstOrDefault(n => n.OperationalStatus == OperationalStatus.Up);
    if (nic != null)
    {
      var ipProps = nic.GetIPProperties();
      var ipv4 = ipProps.UnicastAddresses
                        .FirstOrDefault(x => x.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

      if (ipv4 != null)
      {
        Console.WriteLine($"\nSubnet Address: {ipv4.Address}");
        Console.WriteLine($"Subnet Mask: {ipv4.IPv4Mask}");
      }
    }
  }

  private static bool IsPrivateIp(IPAddress ip)
  {
    var bytes = ip.GetAddressBytes();
    return bytes[0] == 10 || // class A privat
    (bytes[0] == 172 && bytes[1] >= 16 && bytes[1] <= 31) || // class B privat
    (bytes[0] == 192 && bytes[1] == 168); // class C privat
  }
}