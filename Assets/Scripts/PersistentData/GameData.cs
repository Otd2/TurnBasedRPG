using System;
using System.Collections.Generic;
using UnityEngine;

namespace PersistentData
{
    [System.Serializable]
    public class GameData
    {
        public CharactersDictionary CharactersData;
        public int totalMatchCount;
        public List<int> selectedHeroes;
        [SerializeField] public BattleData BattleData;

        public GameData()
        {
            CharactersData = new CharactersDictionary();
            BattleData = null;
        }
    }

    [Serializable]
    public class CharactersDictionary : SerializableDictionary<int, CharacterData> {}
    
    
    [Serializable]
    public class BattleCharacterDictionary : SerializableDictionary<int, int> {}

    [System.Serializable]
    public class BattleData
    {
        public BattleCharacterDictionary CharactersWithHP;
        public int enemyId;
        public int enemyHp;
        public int TurnCount;
    }
    
    [System.Serializable]
    public class CharacterData
    {
        public int Exp;
        public int Lvl;

        public CharacterData(int exp, int lvl)
        {
            Exp = exp;
            Lvl = lvl;
        }
    }

}