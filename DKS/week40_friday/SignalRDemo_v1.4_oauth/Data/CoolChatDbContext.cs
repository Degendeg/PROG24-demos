using Microsoft.EntityFrameworkCore;

public class CoolChatDbContext : DbContext
{
  public CoolChatDbContext(DbContextOptions<CoolChatDbContext> options) : base(options) { }

  public DbSet<User> Users => Set<User>();
  public DbSet<Message> Messages => Set<Message>();
}