namespace HitPoint
{
    public class HealthBase
    {
        protected int _totalHp;

        protected HealthBase(int level, int baseHealth)
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

