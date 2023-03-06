using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [System.Serializable]
    public class GameData
    {
        public CharactersDictionary CharacterData;
        public int totalMatchCount;
        public List<int> selectedHeroes;
        [SerializeField] public BattleData BattleData;

        public GameData()
        {
            CharacterData = new CharactersDictionary();
            BattleData = null;
        }
    }
    
    
    [Serializable]
    public class CharactersDictionary : SerializableDictionary<int, CharacterData> {}
    
    
    [Serializable]
    public class BattleCharacterDictionary : SerializableDictionary<int, int> {}

    public class BattleCharacterData
    {
        private readonly int _id;
        private int _hp;

        public BattleCharacterData(int id, int hp)
        {
            _id = id;
            Hp = hp;
        }

        public int ID => _id;

        public int Hp
        {
            get => _hp;
            set => _hp = value;
        }
    }
    
    
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