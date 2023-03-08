using Character;
using Character.Battle.Controller;
using Character.Battle.View;
using UnityEngine;

namespace Attack
{
    public class AttackBase : IAttack
    {
        protected readonly BattleUnitView AttackSourceView;
        protected readonly float AnimDelay;
        protected readonly float AnimTime;
        private readonly IAttackListener _attackListener;

        public AttackBase(BattleUnitView attackSourceView, float animDelay,
            float animTime, IAttackListener attackListener)
        {
            AttackSourceView = attackSourceView;
            AnimDelay = animDelay;
            AnimTime = animTime;
            _attackListener = attackListener;
        }

        protected void AttackEnd()
        {
            _attackListener.OnAttackEnd();
        }
        
        private void AttackStarted()
        {
            _attackListener.OnAttackStarted();
        }

        public virtual void Execute(int damage)
        {
            AttackStarted();
        }

        protected virtual void ApplyDamage(int damage) { }
    }
}