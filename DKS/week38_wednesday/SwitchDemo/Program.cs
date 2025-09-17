using System.Net.NetworkInformation;

Console.WriteLine("-- ROUTING: Visa IP-konfiguration --");

foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
{
  if (nic.OperationalStatus == OperationalStatus.Up)
  {
    Console.WriteLine($"\nNätverksgränssnitt: {nic.Name}");

    var ipProps = nic.GetIPProperties();

    // Visa IPv4-adresser
    foreach (var ip in ipProps.UnicastAddresses)
    {
      if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
      {
        Console.WriteLine($"IP-adress: {ip.Address}");
        Console.WriteLine($"Nätmask: {ip.IPv4Mask}");
      }
    }
  }
}

Console.WriteLine("\n-- SWITCHING: Visa MAC-adresser --");

foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
{
  var mac = nic.GetPhysicalAddress();
  Console.WriteLine($"\nNätverksgränssnitt: {nic.Name}");
  if (mac != null && mac.GetAddressBytes().Length > 0) // kolla så den innehåller bytes annars skriver den ut tomt
    Console.WriteLine($"MAC-adress: {nic.GetPhysicalAddress()}");
}