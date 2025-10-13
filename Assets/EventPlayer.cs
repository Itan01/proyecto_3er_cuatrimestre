using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlayer : MonoBehaviour
{
    public delegate void EventDelegate(params object[] parameters);

    static Dictionary<EPlayer, EventDelegate> _events = new Dictionary<EPlayer, EventDelegate>();
    public static void Subscribe(EPlayer Name, EventDelegate Method)
    {
        if (_events.ContainsKey(Name))
        {
            _events[Name] += Method;
        }
        else
            _events.Add(Name, Method);
    }
    public static void Unsubscribe(EPlayer Name, EventDelegate Method)
    {
        if (_events.ContainsKey(Name))
        {
            _events[Name] -= Method;
            if (_events[Name] == null)
                _events.Remove(Name);
        }
    }

    public static void Trigger(EPlayer Name, params object[] parameters)
    {
        if (_events.ContainsKey(Name))
        {
            _events[Name](parameters);
        }
    }
}
