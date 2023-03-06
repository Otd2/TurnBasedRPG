using UnityEngine;

namespace DefaultNamespace.Attack
{
    public interface IAttackIncreaseLogicService
    {
        public int GetAttackValue(int baseAttack, int level);
    }

    public class AttackIncreaseLogicService : IAttackIncreaseLogicService
    {
        private readonly float _attackUpgradeOnEachLevel;

        public AttackIncreaseLogicService(int healthUpgradePercentOnEachLevel)
        {
            _attackUpgradeOnEachLevel = healthUpgradePercentOnEachLevel/100f;
        }
        public int GetAttackValue(int baseAttack, int level)
        {
            return Mathf.FloorToInt(baseAttack * (1 + _attackUpgradeOnEachLevel * (1 - level)));
        }
    }
}