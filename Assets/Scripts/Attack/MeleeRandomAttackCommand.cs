using System;
using System.Collections.Generic;
using Character.Battle.View;
using DefaultNamespace.Target;
using DG.Tweening;
using Random = UnityEngine.Random;

namespace Attack
{
    public class MeleeRandomAttackCommand : AttackCommandBase
    {
        private readonly List<ITarget> _possibleTargets;
        private ITarget _target;

        public MeleeRandomAttackCommand(BattleUnitView source, float delay, float animTime, List<ITarget> possibleTargets, int damage, Action onStarted, Action onEnded) : base(source, delay, animTime, damage, onStarted, onEnded)
        {
            _possibleTargets = possibleTargets;
        }

        public override void Execute() 
        {
            base.Execute();
            _target = SelectRandomTarget();
            var startPosition = AttackSourceView.transform.position;
            AttackSourceView.DOKill();
            float halfAnimTime = TotalAnimTime * 0.5f;
            AttackSourceView.transform.DOMove(_target.Position, halfAnimTime)
                .SetDelay(AnimDelay).OnComplete(
                    () =>
                    {
                        ApplyDamage();
                        AttackSourceView.transform.DOMove(startPosition, halfAnimTime).OnComplete(AttackEnd);
                    });
        }

        private ITarget SelectRandomTarget()
        {
            var randIndex = Random.Range(0, _possibleTargets.Count);
            return _possibleTargets[randIndex].IsDead ? SelectRandomTarget() : _possibleTargets[randIndex];
        }

        protected override void ApplyDamage()
        {
            _target.TakeDamage(Damage);
        }
    }
}
