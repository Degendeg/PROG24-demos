using System.Net.NetworkInformation;

public class IpScanner
{
  public async Task<List<string>> ScanAsync(string startIp, string endIp)
  {
    var activeIps = new List<string>();
    uint start = IpRangeHelper.IpToUint(startIp);
    uint end = IpRangeHelper.IpToUint(endIp);

    var tasks = new List<Task>();

    for (uint ip = start; ip <= end; ip++)
    {
      string ipStr = IpRangeHelper.UintToIp(ip);
      tasks.Add(Task.Run(async () =>
      {
        if (await PingHostAsync(ipStr))
        {
          lock (activeIps) // tråd-säker lista
          {
            activeIps.Add(ipStr);
          }
        }
      }));
    }

    await Task.WhenAll(tasks);
    activeIps.Sort(); // valfritt: sortera IPs
    return activeIps;
  }

  private async Task<bool> PingHostAsync(string ipAddress)
  {
    using var ping = new Ping();
    try
    {
      var reply = await ping.SendPingAsync(ipAddress, 1000); // 1 sek timeout
      return reply.Status == IPStatus.Success;
    }
    catch
    {
      return false;
    }
  }
}
