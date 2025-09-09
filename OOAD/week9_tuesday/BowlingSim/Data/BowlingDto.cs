using BowlingSim.Models;

public class BowlingDto
{
  public List<Member> Members { get; set; } = [];
  public List<Match> Matches { get; set; } = [];
}