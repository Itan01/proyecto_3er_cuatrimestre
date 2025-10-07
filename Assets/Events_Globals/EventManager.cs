using System.Collections.Generic;
public static class EventManager
{
    public delegate void EventDelegate(params object[] parameters);

    static Dictionary<EEvents, EventDelegate> _events = new Dictionary<EEvents, EventDelegate>();
    public static void Subscribe(EEvents Name, EventDelegate Method)
    {
        if (_events.ContainsKey(Name))
        {
            _events[Name] += Method;
        }
        else
            _events.Add(Name, Method);
    }
    public static void Unsubscribe(EEvents Name, EventDelegate Method)
    {
        if (_events.ContainsKey(Name))
        {
            _events[Name] -= Method;
            if (_events[Name] == null)
                _events.Remove(Name);
        }
    }

    public static void Trigger(EEvents Name, params object[] parameters)
    {
        if (_events.ContainsKey(Name))
        {
            _events[Name](parameters);
        }
    }
}
