public class GameFacade
{
  private static readonly string[] _itemNames = ["Sword", "Shield", "Wand", "Bow", "Helmet", "Armor"];
  private readonly SingletonLogger _logger = SingletonLogger.Instance;
  private readonly EventSystem _eventSystem = new();

  public void StartGame()
  {
    Console.Write("Enter your name, hero: ");
    string playerName = Console.ReadLine() ?? "Hero";
    Player player = new(playerName);

    _eventSystem.Subscribe("GameStarted", player);
    _eventSystem.Subscribe("ItemReceived", player);
    _eventSystem.Subscribe("LevelUp", player);
    _eventSystem.Subscribe("PlayerDied", player);

    _logger.Log("Game started! 🎮");
    _eventSystem.Notify("GameStarted");

    HandlePlayerChoice(player);
  }

  public void GiveItemToPlayer(Player player, Item item)
  {
    _logger.Log("Item received! 📦");
    player.AddItem(item);
    _eventSystem.Notify("ItemReceived");
  }

  public void LevelUp(Player player)
  {
    player.LevelUp();
    _eventSystem.Notify("LevelUp");
  }

  private void PlayerDied()
  {
    _logger.Log("Player has died! 💀");
    _eventSystem.Notify("PlayerDied");
  }

  private void HandlePlayerChoice(Player player)
  {
    Random random = new();

    while (true) // Loop tills spelare dör
    {
      Console.Write("Choose your path - stay, left or right: ");
      string choice = (Console.ReadLine() ?? "").Trim().ToLower();

      if (string.IsNullOrEmpty(choice))
      {
        Console.WriteLine($"No path chosen, {player.Name}. Try left or right.");
        continue;
      }

      if (choice == "right")
      {
        int outcome = random.Next(0, 2); // 0 eller 1
        if (outcome == 0)
        {
          Item randomItem = new(_itemNames[random.Next(_itemNames.Length)]);
          GiveItemToPlayer(player, randomItem);
          LevelUp(player);
        }
        else
        {
          Console.WriteLine($"{player.Name} encountered a monster and died! 💀");
          PlayerDied();
          break;
        }
      }
      else if (choice == "left")
      {
        int outcome = random.Next(0, 2); // 0 eller 1
        if (outcome == 0)
        {
          Item randomItem = new(_itemNames[random.Next(_itemNames.Length)]);
          GiveItemToPlayer(player, randomItem);
          LevelUp(player);
        }
        else
        {
          Console.WriteLine($"{player.Name} encountered a monster and died! 💀");
          PlayerDied();
          break;
        }
      }
      else if (choice == "stay")
      {
        Console.WriteLine($"Current level: {player.GetLevel()}");
        Console.WriteLine($"Current inventory: {(player.GetInventory().Count > 0 ?
            string.Join(", ", player.GetInventory().Select(i => i.Name).Distinct()) : "Empty")}");
      }
      else
      {
        Console.WriteLine($"Path not found, {player.Name}. Be careful.");
      }
    }
  }
}