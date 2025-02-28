using System.Text.Json;
using BowlingSim.Models;

namespace BowlingSim.Data
{
  public sealed class JsonDb
  {
    private static readonly JsonDb _instance = new();
    private const string FilePath = "db.json";

    public List<Member> Members { get; set; } = [];
    public List<Match> Matches { get; set; } = [];

    private JsonDb()
    {
      Load();
    }

    public static JsonDb Instance => _instance;

    public void Save()
    {
      var data = new BowlingDto
      {
        Members = Members,
        Matches = Matches
      };

      var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
      File.WriteAllText(FilePath, json);
    }

    private void Load()
    {
      if (File.Exists(FilePath))
      {
        var json = File.ReadAllText(FilePath);
        var data = JsonSerializer.Deserialize<BowlingDto>(json);

        if (data != null)
        {
          Members = data.Members ?? [];
          Matches = data.Matches ?? [];
        }
      }
    }
  }
}
