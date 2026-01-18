using System;
using Events.Interfaces;

namespace Events
{
    public class EventLayer : IEventLayer
    {
        public void Fire(string eventName, IEvent eventData = null)
        {
            EventBus.Publish(eventName, eventData);
        }

        public void Subscribe(string eventName, Action<IEvent> eventHandler)
        {
            EventBus.Subscribe(eventName, eventHandler);
        }

        public void Unsubscribe(string eventName, Action<IEvent> eventHandler)
        {
            EventBus.Unsubscribe(eventName, eventHandler);
        }
    }
}