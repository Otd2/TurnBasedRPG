using System;

namespace Events.Interfaces
{
    public interface IEventLayer
    {
        public void Fire(string eventName, IEvent eventData = null);
        public void Subscribe(string eventName, Action<IEvent> eventHandler);
        public void Unsubscribe(string eventName, Action<IEvent> eventHandler);
    }
}