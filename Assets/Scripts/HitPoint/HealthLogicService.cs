using UnityEngine;

namespace HitPoint
{
    public class HealthLogicService : IHealthLogicService
    {
        private float _healthUpgradeOnEachLevel;

        public HealthLogicService(int healthUpgradePercentOnEachLevel)
        {
            _healthUpgradeOnEachLevel = healthUpgradePercentOnEachLevel/100f;
        }

        public int GetTotalHealth(int baseHealth, int level)
        {
            return Mathf.FloorToInt(baseHealth * (1 + _healthUpgradeOnEachLevel * (level - 1)));
        }
    }

    public interface IHealthLogicService
    {
        public int GetTotalHealth(int baseHealth, int level);
    }
}