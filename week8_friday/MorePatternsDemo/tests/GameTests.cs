using NUnit.Framework;
using NUnit.Framework.Internal;

[TestFixture]
public class GameTests
{
  [Test]
  public void Logger_ShouldBeSingleton()
  {
    // Arrange - Hämta två instanser av SingletonLogger
    var logger1 = SingletonLogger.Instance;
    var logger2 = SingletonLogger.Instance;

    // Assert - Kontrollera att båda refs pekar på samma instans
    Assert.That(logger1, Is.SameAs(logger2));
  }

  [Test]
  public void EventSystem_ShouldNotifyListeners()
  {
    // Arrange - Skapa EventSystem och en testlyssnare
    var eventSystem = new EventSystem();
    var testListener = new TestListener();
    eventSystem.Subscribe("TestEvent", testListener);

    // Act - Utlös event
    eventSystem.Notify("TestEvent");

    // Assert - Kontrollera att lyssnaren mottagit event
    Assert.That(testListener.EventReceieved, Is.True);
  }

  [Test]
  public void Player_ShouldStartWithCorrectNameAndLevel()
  {
    // Arrange - Skapa en spelare med ett namn
    Player player = new("TestHero");

    // Assert - Kontrollera att namn och startlevel är korrekta
    Assert.That(player.Name, Is.EqualTo("TestHero"));
    Assert.That(player.GetLevel(), Is.EqualTo(1));
  }

  [Test]
  public void Player_ShouldReceiveItemAndLevelUpOnRightPath()
  {
    // Arrange - Skapa spelare, spel-setup och item
    Player player = new("TestHero");
    GameFacade game = new();
    Item item = new("Bow");
    int initialLevel = player.GetLevel();

    // Act - Ge spelaren ett item och öka level
    game.GiveItemToPlayer(player, item);
    game.LevelUp(player);

    // Assert - Kontrollera att spelaren har gått upp en level och fått item
    Assert.That(player.GetLevel(), Is.EqualTo(initialLevel + 1));
    Assert.That(player.GetInventory().Exists(i => i.Name == "Bow"), Is.True);
  }

  /// <summary>
  /// Det här är en teststub som implementerar IGameEventListener för att verifiera att eventsystemet fungerar korrekt i testerna.
  /// </summary>
  private class TestListener : IGameEventListener
  {
    // Håller koll på om en händelse har mottagits
    public bool EventReceieved { get; private set; } = false;

    // Sätts till true när en händelse tas emot
    public void OnEvent(string eventType) => EventReceieved = true;
  }
}