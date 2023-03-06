using UnityEngine;

namespace Level
{
    public class LevelUpLogicService : ILevelUpLogicService
    {
        private int _levelUpXp;

        public LevelUpLogicService(int levelUpXp)
        {
            _levelUpXp = levelUpXp;
        }

        public bool IsLevelUp(int xp, int level)
        {
            var currentLevel = 1 + Mathf.FloorToInt((float)xp / _levelUpXp);
            return currentLevel > level;
        }
    }
}