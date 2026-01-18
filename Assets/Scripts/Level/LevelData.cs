using Core;
using Level.Services;

 namespace Level
{
    public class LevelData
    {
        private int _xp;
        private int _level;
        public int Level => _level;
        public int Xp => _xp;
        public LevelData(int xp, int level)
        {
            _xp = xp;
            _level = level;
        }

        public void AddXp(int earnedXp)
        {
            _xp += earnedXp;
            if (ServiceLocator.Instance.Get<ILevelUpLogicService>().IsLevelUp(_xp, _level))
            {
                _level++;
            }
        }
    }
}