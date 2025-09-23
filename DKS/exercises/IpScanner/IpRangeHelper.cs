using System;
using System.Net;

public static class IpRangeHelper
{
  // Konverterar IP-sträng till uint
  public static uint IpToUint(string ipAddress)
  {
    var bytes = IPAddress.Parse(ipAddress).GetAddressBytes();
    if (BitConverter.IsLittleEndian)
      Array.Reverse(bytes);
    return BitConverter.ToUInt32(bytes);
  }

  // Konverterar uint till IP-sträng
  public static string UintToIp(uint ip)
  {
    var bytes = BitConverter.GetBytes(ip);
    if (BitConverter.IsLittleEndian)
      Array.Reverse(bytes);
    return new IPAddress(bytes).ToString();
  }
}
