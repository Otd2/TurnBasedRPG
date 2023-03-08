using System;
using System.Collections.Generic;
using Character;
using Character.Battle.Controller;
using Character.Battle.View;
using DefaultNamespace.Target;

namespace Attack
{
    public static class AttackFactory
    {
        public static AttackBase GetAttackStrategy(BattleUnitView source, 
            AttackType type, List<ITarget> targets, IAttackListener attackListener)
        {
            switch (type)
            {
                case AttackType.MeleeTargeted:
                    return new MeleeTargetedAttack(source, 0, 1f, targets[0], attackListener);
                case AttackType.MeleeRandom:
                    return new MeleeRandomAttack(source, 0, 1f, targets, attackListener);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}