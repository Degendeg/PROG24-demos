using System.Net;
using FluentFTP;

class FtpDemo
{
  public static void Run()
  {
    System.Console.WriteLine("FTP Demo:");
    try
    {
      using var client = new FtpClient("ftp.dlptest.com", new NetworkCredential("dlpuser", "rNrKYTX9g7z3RgJRmxWuGHbeu"));
      client.Connect();

      string localFile = "prog24.txt";
      File.WriteAllText(localFile, "Hello FTP!!!");

      client.UploadFile(localFile, "/prog24.txt");
      Console.WriteLine("Upload complete!");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"FTP Error {ex.Message}");
    }
  }
}