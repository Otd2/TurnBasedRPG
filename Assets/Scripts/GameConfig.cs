using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    [System.Serializable]
    public class GameConfig
    {
        public int[] firstUnlockedHeroes = new[] { 1, 2, 3 };
        public int levelUpOnEachXp = 5; 
        public int characterUnlockAfterBattle = 5;
        public int attackIncreasePercentOnEachLevel = 10; 
        public int healthIncreasePercentOnEachLevel = 10; 
        public CharacterUIView characterUIView;
        public HeroView heroView;
        public BattleUnitView battleView;

    }
}