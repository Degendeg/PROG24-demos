namespace BowlingSim.Models
{
  public class Match
  {
    public int Id { get; set; }
    public Member Player1 { get; set; }
    public Member Player2 { get; set; }
    public int Score1 { get; set; }
    public int Score2 { get; set; }

    public Match(int id, Member player1, Member player2)
    {
      Id = id;
      Player1 = player1;
      Player2 = player2;
      Score1 = 0;
      Score2 = 0;
    }
  }
}
