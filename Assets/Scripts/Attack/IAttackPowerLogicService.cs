using UnityEngine;

namespace Attack
{
    public interface IAttackPowerLogicService : IService
    {
        int GetAttackValue(int baseAttack, int level);
    }

    public class AttackPowerLogicService : IAttackPowerLogicService
    {
        private readonly float _attackUpgradeOnEachLevel;

        public AttackPowerLogicService(int attackUpgradePercent)
        {
            _attackUpgradeOnEachLevel = attackUpgradePercent / 100f;
        }

        public void Initialize() { }

        public int GetAttackValue(int baseAttack, int level)
        {
            return Mathf.FloorToInt(baseAttack * (1 + _attackUpgradeOnEachLevel * (level - 1)));
        }
    }
}
