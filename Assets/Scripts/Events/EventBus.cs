using System;
using System.Collections.Generic;
using Events.Interfaces;

namespace Events
{
    public static class EventBus
    {
        private static readonly Dictionary<string, List<Action<IEvent>>> EVENT_HANDLERS = new();

        public static void Subscribe(string eventName, Action<IEvent> handler)
        {
            if (!EVENT_HANDLERS.ContainsKey(eventName))
                EVENT_HANDLERS[eventName] = new List<Action<IEvent>>();
            
            EVENT_HANDLERS[eventName].Add(handler);
        }

        public static void Unsubscribe(string eventName, Action<IEvent> handler)
        {
            if (EVENT_HANDLERS.TryGetValue(eventName, out List<Action<IEvent>> subscribers))
                subscribers.Remove(handler);
        }

        public static void Publish(string eventName, IEvent eventData = null)
        {
            if (!EVENT_HANDLERS.TryGetValue(eventName, out List<Action<IEvent>> subscribers))
                return;

            foreach (var handler in subscribers)
                handler?.Invoke(eventData);
        }

        public static void Clear() => EVENT_HANDLERS.Clear();
    }
}
