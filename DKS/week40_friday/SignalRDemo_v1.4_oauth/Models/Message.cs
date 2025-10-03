using System.ComponentModel.DataAnnotations;

public class Message
{
  public int Id { get; set; }
  [Required]
  public string Text { get; set; } = string.Empty;
  public DateTime SentAt { get; set; } = DateTime.UtcNow;
  public int UserId { get; set; }
  public User? User { get; set; }
}