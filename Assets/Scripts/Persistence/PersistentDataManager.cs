using Core;
using System;
using System.Collections.Generic;
using Character;
using Config;

namespace Persistence
{
    public abstract class PersistentDataManager
    {
        public GameData CurrentGameData;
        
        public void OnCharacterHPChanged(int id, int hp)
        {
            CurrentGameData.BattleData.CharactersWithHP[id] = hp;
        }
        
        public void OnCharacterUnlocked(int id)
        {
            CurrentGameData.CharactersData.Add(id, new CharacterData(0,1));
        }

        public CharacterData GetCharacterData(int id)
        {
            return CurrentGameData.CharactersData.ContainsKey(id) ? CurrentGameData.CharactersData[id] : null;
        }

        public CharactersDictionary GetCharactersData()
        {
            return CurrentGameData.CharactersData;
        }
        
        public  BattleData GetBattleData()
        {
            return CurrentGameData.BattleData;
        }
        
        public bool IsBattleOn()
        {
            return !(CurrentGameData.BattleData == null || CurrentGameData.BattleData.CharactersWithHP.Count == 0);
        }
        
        protected void CreateNewSaveData()
        {
            var config = ServiceLocator.Instance.Get<IGameConfigService>().Config;
            CurrentGameData = new GameData();
            CurrentGameData.selectedHeroes = new List<int>();
            foreach (CharacterAttributesSo configFirstUnlockedHero in config.FirstUnlockedHeroes)
            {
                CurrentGameData.selectedHeroes.Add(configFirstUnlockedHero.ID);
                CurrentGameData.CharactersData.Add(configFirstUnlockedHero.ID, new CharacterData(0, 1));
            }
            Save();
        }
        
        public abstract void Load(Action onLoadCompleted);
        public abstract void Save();

    }
}