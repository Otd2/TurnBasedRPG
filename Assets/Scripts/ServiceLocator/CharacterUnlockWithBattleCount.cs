using PersistentData;
using UnityEngine;

public class CharacterUnlockWithBattleCount : ICharacterUnlockLogicService
{
    private readonly int _unlockEveryBattle;
    private readonly PersistantDataManager _persistantDataManager;

    public CharacterUnlockWithBattleCount(int unlockEveryBattle, 
        PersistantDataManager persistantDataManager)
    {
        _unlockEveryBattle = unlockEveryBattle;
        _persistantDataManager = persistantDataManager;
    }

    public bool IsNewCharacterUnlock()
    {
        var currentUnlockedHeroCount = _persistantDataManager.CurrentGameData.CharactersData.Count;
        if (currentUnlockedHeroCount >= 10)
            return false;

        var neededUnlockedHeroCount = 3 + 
                                      Mathf.FloorToInt((float)_persistantDataManager.CurrentGameData.totalMatchCount / _unlockEveryBattle);
        return currentUnlockedHeroCount < neededUnlockedHeroCount;
    }
}


public interface ICharacterUnlockLogicService
{
    public bool IsNewCharacterUnlock();
}