using System.Collections.Generic;
using Character;
using Character.Battle.Controller;
using Character.Battle.View;
using DefaultNamespace.Target;
using DG.Tweening;
using UnityEngine;

namespace Attack
{
    public class MeleeRandomAttack : AttackBase
    {
        private readonly List<ITarget> _possibleTargets;
        private ITarget _target;

        public MeleeRandomAttack(BattleUnitView source, float delay, float animTime, List<ITarget> possibleTargets,
            IAttackListener attackListener) 
            : base(source, delay, animTime, attackListener)
        {
            _possibleTargets = possibleTargets;
        }

        public override void Execute(int damage) 
        {
            base.Execute(damage);
            _target = SelectRandomTarget();
            var startPosition = AttackSourceView.transform.position;
            AttackSourceView.DOKill();
            AttackSourceView.transform.DOMove(_target.Position, AnimTime/2f)
                .SetDelay(AnimDelay).OnComplete(
                    () =>
                    {
                        ApplyDamage(damage);
                        AttackSourceView.transform.DOMove(startPosition, AnimTime/2f).OnComplete(AttackEnd);
                    });
        }

        private ITarget SelectRandomTarget()
        {
            var randIndex = Random.Range(0, _possibleTargets.Count);
            return _possibleTargets[randIndex].IsDead ? SelectRandomTarget() : _possibleTargets[randIndex];
        }

        protected override void ApplyDamage(int damage)
        {
            _target.TakeDamage(damage);
        }
    }
}