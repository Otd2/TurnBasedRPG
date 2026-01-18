using System;
using Character.Battle.View;
using Core.Target;
using DG.Tweening;

namespace Combat.Commands
{
    public class MeleeTargetedAttackCommand : AttackCommandBase
    {
        private readonly ITarget _target;

        public MeleeTargetedAttackCommand(BattleUnitView source, float delay, float animTime, ITarget target, int damage, Action onStarted, Action onEnded) : base(source, delay, animTime, damage, onStarted, onEnded)
        {
            _target = target;
        }

        public override void Execute()
        {
            base.Execute();
            AttackSourceView.DOKill();
            var startPosition = AttackSourceView.transform.position;
            float halfAnimTime = TotalAnimTime * 0.5f;
            AttackSourceView.transform.DOMove(_target.Position, halfAnimTime)
                .SetDelay(AnimDelay).OnComplete(
                    () =>
                    {
                        ApplyDamage();
                        AttackSourceView.transform.DOMove(startPosition, halfAnimTime).OnComplete(AttackEnd);
                    });
        }

        protected override void ApplyDamage()
        {
            _target.TakeDamage(Damage);
        }
    }
}
