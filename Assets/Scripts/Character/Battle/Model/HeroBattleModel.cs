﻿using Character.Battle.Model;
using PersistentData;

namespace Character
{
    public class HeroBattleModel : UnitBattleModel 
    {
        public HeroBattleModel(int id, int level, int xp, 
            CharacterAttributes attributes,
            PersistantDataManager persistentDataManager) :
            base(id, level, xp, attributes, persistentDataManager)
        {
            hp.HealthChange(persistentDataManager.GetBattleData().CharactersWithHP[id]);
        }
        
        public void RewardEarned()
        {
            LevelDataLogic.AddXp(ServiceLocator.Instance.EndGameReward.GetRewardedExp());
            PersistentDataManager.GetCharacterData(Id).Exp = LevelDataLogic.Xp;
            PersistentDataManager.GetCharacterData(Id).Lvl = LevelDataLogic.Level;
        }
    }
}