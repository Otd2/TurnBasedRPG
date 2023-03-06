using UnityEngine;

namespace DefaultNamespace.Target
{
    public interface ITarget
    {
        public Vector3 Position { get; }
        public bool IsDead { get; }
        public void TakeDamage(int damage);
    }
}