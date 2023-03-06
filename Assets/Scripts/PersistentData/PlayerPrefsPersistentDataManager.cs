using System;
using System.Collections.Generic;
using Character;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerPrefsPersistentDataManager
    {
        private readonly GameConfig _config;
        public GameData CurrentGameData;
        public const string SaveKey = "saveData";
        
        public PlayerPrefsPersistentDataManager(GameConfig config)
        {
            _config = config;
        }
        
        public void OnCharacterUnlocked(int id)
        {
            CurrentGameData.CharacterData.Add(id, new CharacterData(0,1));
        }

        public void OnCharacterHPChanged(int id, int newHp)
        {
            CurrentGameData.BattleData.CharactersWithHP[id] = newHp;
            SaveData();
        }

        public CharacterData GetCharacterData(int id)
        {
            if (CurrentGameData.CharacterData.ContainsKey(id))
            {
                return CurrentGameData.CharacterData[id];
            }

            return null;
        }

        public void LoadData(Action onLoadCompleted)
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                //Make it abstract to support different save methods such as cloud, local etc.
                var saveString = PlayerPrefs.GetString(SaveKey);
                CurrentGameData = JsonUtility.FromJson<GameData>(saveString);
            }
            else
                CreateNewSaveData();
            
            onLoadCompleted.Invoke();
        }

        private void CreateNewSaveData()
        {
            CurrentGameData = new GameData();
            CurrentGameData.selectedHeroes = new List<int>();
            for (var index = 0; index < _config.firstUnlockedHeroes.Length; index++)
            {
                var firstUnlockedHero = _config.firstUnlockedHeroes[index];
                CurrentGameData.selectedHeroes.Add(firstUnlockedHero);
                CurrentGameData.CharacterData.Add(firstUnlockedHero, new CharacterData(0, 1));
            }
            SaveData();
        }
        
        public void SaveData()
        {
            PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(CurrentGameData, true));
            PlayerPrefs.Save();
        }
    }

    
}