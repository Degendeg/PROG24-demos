using System.ComponentModel.DataAnnotations;

public class User
{
  public int Id { get; set; }
  [Required, MaxLength(50)]
  public string Username { get; set; } = string.Empty;

  [Required]
  public string PasswordHash { get; set; } = string.Empty;

  public ICollection<Message> Messages { get; set; } = new List<Message>();
}