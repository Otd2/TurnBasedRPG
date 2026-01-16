using Events.Interfaces;
using UnityEngine;

namespace Events
{
    public class DamageReceivedEvent : IEvent
    {
        public int Damage { get; }
        public Vector3 Position { get; }

        public DamageReceivedEvent(int damage, Vector3 position)
        {
            Damage = damage;
            Position = position;
        }
    }
}
