using PatternsDemo.Core;

namespace PatternsDemo.Utils
{
  public class ConsoleLogger : ILogger
  {
    public void Log(string message) => Console.WriteLine($"Log: {message}");
  }
}