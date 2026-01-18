using Character.Interfaces;
using Persistence;
using UnityEngine;

namespace Character.Services
{
    public class CharacterUnlockService : ICharacterUnlockLogicService
    {
        private readonly int _unlockEveryBattle;
        private readonly PersistentDataManager _persistantDataManager;

        public CharacterUnlockService(int unlockEveryBattle, PersistentDataManager persistantDataManager)
        {
            _unlockEveryBattle = unlockEveryBattle;
            _persistantDataManager = persistantDataManager;
        }

        public void Initialize() { }

        public bool IsNewCharacterUnlock()
        {
            var currentUnlockedHeroCount = _persistantDataManager.CurrentGameData.CharactersData.Count;
            if (currentUnlockedHeroCount >= 10)
                return false;

            int neededUnlockedHeroCount = 3 + Mathf.FloorToInt((float)_persistantDataManager.CurrentGameData.totalMatchCount / _unlockEveryBattle);
            return currentUnlockedHeroCount < neededUnlockedHeroCount;
        }
    }
}
