using Character;
using DefaultNamespace.Target;
using UnityEngine;

namespace DefaultNamespace.Attack
{
    public class AttackBase : IAttack
    {
        //Attack Events
        public delegate void OnAttackEndEvent();
        public event OnAttackEndEvent OnAttackEnd;
        
        public delegate void OnAttackStartedEvent();
        public event OnAttackStartedEvent OnAttackStarted;

        public delegate void OnApplyDamageEvent();
        public event OnApplyDamageEvent OnApplyDamage;

        protected void AttackEnd()
        {
            OnAttackEnd?.Invoke();
        }
        
        private void AttackStarted()
        {
            OnAttackStarted?.Invoke();
        }
        
        public virtual void ApplyDamage(int damage)
        {
            OnApplyDamage?.Invoke();
        }

        public virtual void Execute(int damage)
        {
            AttackStarted();
        }

    }
}