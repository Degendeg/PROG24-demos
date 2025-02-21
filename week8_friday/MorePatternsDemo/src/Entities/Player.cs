public class Player(string name) : IGameEventListener
{
  public string Name = name;
  private readonly SingletonLogger _logger = SingletonLogger.Instance;
  private int _level = 1;
  private readonly List<Item> _inventory = [];
  public int GetLevel() => _level;
  public List<Item> GetInventory() => _inventory;

  public void AddItem(Item item)
  {
    _inventory.Add(item);
    _logger.Log($"{Name} received item: {item.Name}");
  }
  public void LevelUp()
  {
    _level++;
    _logger.Log($"{Name} leveled up! Now at Level {_level} 🎉");
  }
  public void OnEvent(string eventType)
  {
    _logger.Log($"{Name} received event: {eventType}");
  }
}