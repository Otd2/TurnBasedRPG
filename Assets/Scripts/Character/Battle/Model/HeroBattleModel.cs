using Core;
using Persistence;
using Reward;

namespace Character.Battle.Model
{
    public class HeroBattleModel : UnitBattleModel 
    {
        public HeroBattleModel(int id, int level, int xp, CharacterAttributes attributes, PersistentDataManager persistentDataManager) : base(id, level, xp, attributes, persistentDataManager)
        {
            hp.HealthChange(persistentDataManager.GetBattleData().CharactersWithHP[id]);
        }
        
        public void RewardEarned()
        {
            LevelDataLogic.AddXp(ServiceLocator.Instance.Get<IRewardService>().GetRewardedExp());
            PersistentDataManager.GetCharacterData(Id).Exp = LevelDataLogic.Xp;
            PersistentDataManager.GetCharacterData(Id).Lvl = LevelDataLogic.Level;
        }
    }
}