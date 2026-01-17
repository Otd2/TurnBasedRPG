using System;
using System.Collections.Generic;
using Character.Battle.View;
using DefaultNamespace.Target;

namespace Attack
{
    public static class AttackCommandFactory
    {
        public static AttackCommandBase Create(BattleUnitView source, AttackType type, List<ITarget> targets, int damage, Action onStarted, Action onEnded, float delay = 0f, float totalAnimationTime = 1f)
        {
            return type switch
            {
                AttackType.MeleeTargeted => new MeleeTargetedAttackCommand(source, delay, totalAnimationTime, targets[0], damage, onStarted, onEnded),
                AttackType.MeleeRandom => new MeleeRandomAttackCommand(source, delay, totalAnimationTime, targets, damage, onStarted, onEnded),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}
