using System;
using Character.Battle.View;

namespace Combat.Commands
{
    public class AttackCommandBase : ICommand
    {
        protected readonly BattleUnitView AttackSourceView;
        protected readonly float AnimDelay;
        protected readonly float TotalAnimTime;
        protected readonly int Damage;
        
        private readonly Action _onStarted;
        private readonly Action _onEnded;

        protected AttackCommandBase(BattleUnitView attackSourceView, float animDelay, float totalAnimTime, int damage, Action onStarted, Action onEnded)
        {
            AttackSourceView = attackSourceView;
            AnimDelay = animDelay;
            TotalAnimTime = totalAnimTime;
            Damage = damage;
            _onStarted = onStarted;
            _onEnded = onEnded;
        }

        protected void AttackEnd() => _onEnded?.Invoke();

        public virtual void Execute()
        {
            _onStarted?.Invoke();
        }

        protected virtual void ApplyDamage() { }
    }
}
