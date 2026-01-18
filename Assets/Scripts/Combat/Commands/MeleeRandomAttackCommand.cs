using System;
using System.Collections.Generic;
using System.Linq;
using Character.Battle.View;
using Core.Target;
using DG.Tweening;
using Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Combat.Commands
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

            if (_target == null)
            {
                EventBus.Publish(EventNames.Errors.NoTargetToAttack);
                return;
            }
            
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
            if (_possibleTargets.All(target => target.IsDead))
            {
                Debug.LogError("All targets are dead. This should not happen.");
                return null;
            }
            
            List<ITarget> aliveTargets = _possibleTargets.Where(target => !target.IsDead).ToList();
            return aliveTargets[Random.Range(0, aliveTargets.Count)];
        }

        protected override void ApplyDamage()
        {
            _target.TakeDamage(Damage);
        }
    }
}
