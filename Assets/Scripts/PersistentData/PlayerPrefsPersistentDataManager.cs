using System;
using UnityEngine;

namespace PersistentData
{
    public class PlayerPrefsPersistentDataManager : PersistantDataManager
    {
        private const string SaveKey = "saveData";

        public override void Load(Action onLoadCompleted)
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                var saveString = PlayerPrefs.GetString(SaveKey);
                CurrentGameData = JsonUtility.FromJson<GameData>(saveString);
            }
            else
                CreateNewSaveData();
            
            onLoadCompleted.Invoke();
        }

        public override void Save()
        {
            PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(CurrentGameData, true));
            PlayerPrefs.Save();
        }
    }
}