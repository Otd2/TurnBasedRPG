using Combat.Services;
using Health.Services;
using Core;
﻿namespace Health
{
    public class HealthBase
    {
        protected int _totalHp;

        protected HealthBase(int level, int baseHealth)
        {
            _totalHp = ServiceLocator.Instance.Get<IHealthLogicService>().GetTotalHealth(baseHealth, level);
        }

        public virtual int GetHp()
        {
            return _totalHp;
        }

        public int TotalHp => _totalHp;
    }
}

