using Character;
using DefaultNamespace.Target;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Attack
{
    public class MeleeTargetedAttack : AttackBase
    {
        private readonly Transform _source;
        private readonly ITarget _target;
        private readonly float _delay;
        private readonly float _animTime;

        public MeleeTargetedAttack(Transform source, ITarget target,  float delay, float animTime) 
        {
            _source = source;
            _target = target;
            _delay = delay;
            _animTime = animTime;
        }

        public override void Execute(int damage)
        {
            base.Execute(damage);
            _source.DOKill();
            var startPosition = _source.position;
            _source.transform.DOMove(_target.Position, _animTime/2f)
                .SetDelay(_delay).OnComplete(
                    () =>
                    {
                        ApplyDamage(damage);
                        _source.transform.DOMove(startPosition, _animTime/2f).OnComplete(AttackEnd);
                    });
        }
        
        
        public override void ApplyDamage(int damage)
        {
            base.ApplyDamage(damage);
            _target.TakeDamage(damage);
        }
        
        
    }
}