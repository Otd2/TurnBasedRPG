using Character;
using Character.Battle.Controller;
using Character.Battle.View;
using DefaultNamespace.Target;
using DG.Tweening;

namespace Attack
{
    public class MeleeTargetedAttack : AttackBase
    {
        private readonly ITarget _target;

        public MeleeTargetedAttack(BattleUnitView source, float delay, float animTime, ITarget target,
            IAttackListener attackListener) 
            : base(source, delay, animTime, attackListener)
        {
            _target = target;
        }

        public override void Execute(int damage)
        {
            base.Execute(damage);
            AttackSourceView.DOKill();
            var startPosition = AttackSourceView.transform.position;
            AttackSourceView.transform.DOMove(_target.Position, AnimTime/2f)
                .SetDelay(AnimDelay).OnComplete(
                    () =>
                    {
                        ApplyDamage(damage);
                        AttackSourceView.transform.DOMove(startPosition, AnimTime/2f).OnComplete(AttackEnd);
                    });
        }

        protected override void ApplyDamage(int damage)
        {
            _target.TakeDamage(damage);
        }
        
        
    }
}