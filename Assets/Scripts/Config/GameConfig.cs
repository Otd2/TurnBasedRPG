using System;
using Character;
using UnityEngine;

namespace Config
{
    [Serializable]
    public class GameConfig
    {
        [Header("Initial Setup")] 
        public CharacterAttributesSo[] FirstUnlockedHeroes;
    
        [Header("Leveling")]
        public int LevelUpOnEachXp = 5;
        public int ExpPerWin = 1;
    
        [Header("Character Unlock")]
        public int CharacterUnlockAfterBattle = 5;
    
        [Header("Stats Per Level")]
        [Range(0, 100)]
        public int AttackIncreasePercentOnEachLevel = 10;
        [Range(0, 100)]
        public int HealthIncreasePercentOnEachLevel = 10;
    
        [Header("Battle")]
        public int RequiredHeroCountForBattle = 3;
        public int MaxHeroCount = 10;
    }
}