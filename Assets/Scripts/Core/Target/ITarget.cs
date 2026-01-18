using UnityEngine;

namespace Core.Target
{
    public interface ITarget
    {
        public Vector3 Position { get; }
        public bool IsDead { get; }
        public void TakeDamage(int damage);
    }
}