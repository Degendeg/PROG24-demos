public class EventSystem
{
  private readonly Dictionary<string, List<IGameEventListener>> _listeners = [];

  public void Subscribe(string eventType, IGameEventListener listener)
  {
    if (!_listeners.ContainsKey(eventType))
    {
      _listeners[eventType] = [];
    }
    _listeners[eventType].Add(listener);
  }

  public void Notify(string eventType)
  {
    if (_listeners.ContainsKey(eventType))
    {
      foreach (var listener in _listeners[eventType])
      {
        listener.OnEvent(eventType);
      }
    }
  }
}