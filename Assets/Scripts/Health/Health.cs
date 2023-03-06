using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.Health
{
    public class Health
    {
        protected int _totalHp;

        public Health(int level, int baseHealth)
        {
            _totalHp = ServiceLocator.Instance.HealthLogicService.GetTotalHealth(baseHealth, level);
        }

        public virtual int GetHp()
        {
            return _totalHp;
        }

        public int TotalHp => _totalHp;
    }
}

