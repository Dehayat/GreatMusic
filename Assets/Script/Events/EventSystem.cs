using System;
using System.Collections.Generic;

public class EventData
{

}


public class EventSystem
{
    private static EventSystem instance;

    public static EventSystem GetInstance()
    {
        if (instance == null)
        {
            instance = new EventSystem();
        }
        return instance;
    }

    private Dictionary<string, Action<EventData>> events;

    public EventSystem()
    {
        events = new Dictionary<string, Action<EventData>>();
    }

    public void ListenToEvent(string eventName, Action<EventData> eventFunction)
    {
        if (!events.ContainsKey(eventName))
        {
            events.Add(eventName, null);
        }
        events[eventName] += eventFunction;
    }
    public void IgnoreEvent(string eventName, Action<EventData> eventFunction)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName] -= eventFunction;
        }
    }
    public void EmitEvent(string eventName, EventData eventData)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName]?.Invoke(eventData);
        }
    }

}
