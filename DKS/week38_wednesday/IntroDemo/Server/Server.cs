using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
  public static void Run()
  {
    var listener = new TcpListener(IPAddress.Any, 5000);
    listener.Start();
    Console.WriteLine("Server started. Waiting for client..");

    using var client = listener.AcceptTcpClient();
    Console.WriteLine("Client connected");
    using var stream = client.GetStream();

    while (stream.CanRead)
    {
      // läsa från klienten
      byte[] buffer = new byte[1024];
      int bytesRead = stream.Read(buffer, 0, buffer.Length);
      string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
      Console.WriteLine($"Client: {message}");

      if (message.ToLower() == "exit")
      {
        Console.WriteLine("Client ended the connection");
        break;
      }

      // svara klienten
      string response = new Random().Next(99).ToString();
      byte[] responseBytes = Encoding.UTF8.GetBytes(response);
      stream.Write(responseBytes);
    }

    listener.Stop();
  }
}