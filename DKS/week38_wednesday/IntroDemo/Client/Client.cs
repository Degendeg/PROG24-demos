using System.Net.Sockets;
using System.Text;

class Client
{
  public static void Run()
  {
    using var client = new TcpClient("127.0.0.1", 5000);
    using var stream = client.GetStream();

    Console.WriteLine("Connected to server. Type 'exit' to quit.");

    while (stream.CanWrite)
    {
      // Skicka till servern
      Console.Write("You: ");
      string message = Console.ReadLine() ?? "";
      byte[] data = Encoding.UTF8.GetBytes(message);
      stream.Write(data);

      if (message.ToLower() == "exit")
        break;

      // Läsa svaret från servern
      byte[] buffer = new byte[1024];
      int bytesRead = stream.Read(buffer, 0, buffer.Length);
      string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
      Console.WriteLine($"Server: {response}");
    }
  }
}