using UnityEngine;

namespace Level.Services
{
    public class LevelUpLogicService : ILevelUpLogicService
    {
        private readonly int _levelUpXp;

        public LevelUpLogicService(int levelUpXp)
        {
            _levelUpXp = levelUpXp;
        }

        public void Initialize() { }

        public bool IsLevelUp(int xp, int level)
        {
            var currentLevel = 1 + Mathf.FloorToInt((float)xp / _levelUpXp);
            return currentLevel > level;
        }
    }
}
