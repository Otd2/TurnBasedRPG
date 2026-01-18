using System;

namespace Health
{
    public class HealthPoints : HealthBase
    {
        private int value;
        public delegate void OnHealthChangedEvent(int newHealth);
        public event OnHealthChangedEvent OnHealthChanged;

        public HealthPoints(int level, int baseHealth) : base(level, baseHealth)
        { }

        public override int GetHp()
        {
            return value;
        }

        public void HealthChange(int newHealth)
        {
            value = Math.Max(newHealth, 0);
            OnHealthChanged?.Invoke(value);
        }
    }
}