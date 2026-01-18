using Core;
using UnityEngine;

namespace Health.Services
{
    public interface IHealthLogicService : IService
    {
        int GetTotalHealth(int baseHealth, int level);
    }

    public class HealthLogicService : IHealthLogicService
    {
        private float _healthUpgradeOnEachLevel;

        public HealthLogicService(int healthUpgradePercentOnEachLevel)
        {
            _healthUpgradeOnEachLevel = healthUpgradePercentOnEachLevel / 100f;
        }

        public void Initialize() { }

        public int GetTotalHealth(int baseHealth, int level)
        {
            return Mathf.FloorToInt(baseHealth * (1 + _healthUpgradeOnEachLevel * (level - 1)));
        }
    }
}
