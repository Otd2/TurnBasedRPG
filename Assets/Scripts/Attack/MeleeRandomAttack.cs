using System.Collections.Generic;
using DefaultNamespace.Target;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Attack
{
    public class MeleeRandomAttack : AttackBase
    {
        private readonly Transform _source;
        private readonly float _delay;
        private readonly float _animTime;
        private readonly List<ITarget> _possibleTargets;
        private ITarget _target;

        public MeleeRandomAttack(Transform source, List<ITarget> possibleTargets,  float delay, float animTime)
        {
            _source = source;
            _delay = delay;
            _animTime = animTime;
            _possibleTargets = possibleTargets;
        }

        public override void Execute(int damage) 
        {
            base.Execute(damage);
            _target = SelectRandomTarget();
            var startPosition = _source.position;
            _source.DOKill();
            _source.transform.DOMove(_target.Position, _animTime/2f)
                .SetDelay(_delay).OnComplete(
                    () =>
                    {
                        ApplyDamage(damage);
                        _source.transform.DOMove(startPosition, _animTime/2f).OnComplete(AttackEnd);
                    });
        }

        private ITarget SelectRandomTarget()
        {
            var randIndex = Random.Range(0, _possibleTargets.Count);
            return _possibleTargets[randIndex].IsDead ? SelectRandomTarget() : _possibleTargets[randIndex];
        }

        public override void ApplyDamage(int damage)
        {
            base.ApplyDamage(damage);
            _target.TakeDamage(damage);
        }
    }
}