using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

[Authorize]
public class ChatHub : Hub
{
  private readonly CoolChatDbContext _db;
  public ChatHub(CoolChatDbContext db)
  {
    _db = db;
  }

  public async Task SendMessage(string message)
  {
    try
    {
      var username = Context.User?.Identity?.Name ?? "Unknown";
      var user = _db.Users.FirstOrDefault(u => u.Username == username);

      if (user == null) return;

      var msg = new Message
      {
        Text = message,
        UserId = user.Id
      };

      _db.Messages.Add(msg);
      await _db.SaveChangesAsync();

      await Clients.All.SendAsync("ReceiveMessage", username, message);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Fel: {ex}");
      throw;
    }
  }
}