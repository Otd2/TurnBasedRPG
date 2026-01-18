using System;
using Events.Interfaces;
using UnityEngine;

namespace Events
{
    public abstract class MonoBehaviourEventLayer : MonoBehaviour, IEventLayer
    {
        public void Subscribe(string eventName, Action<IEvent> handler)
        {
            EventBus.Subscribe(eventName, handler);
        }

        public void Unsubscribe(string eventName, Action<IEvent> handler)
        {
            EventBus.Unsubscribe(eventName, handler);
        }

        public void Fire(string eventName, IEvent eventData = null)
        {
            EventBus.Publish(eventName, eventData);
        }
    }
}
