class HttpDemo
{
  public static async Task Run()
  {
    try
    {
      using HttpClient client = new();
      var res = await client.GetStringAsync("https://jsonplaceholder.typicode.com/posts/1");
      Console.WriteLine("Response:\n " + res);
    }
    catch (Exception ex)
    {
      Console.WriteLine("HTTP Error: " + ex);
    }
  }
}