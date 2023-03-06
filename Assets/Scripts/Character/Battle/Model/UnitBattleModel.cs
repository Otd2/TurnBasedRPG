using DefaultNamespace.Health;

namespace Character
{
    public class UnitBattleModel : UnitModelBase
    {
        public bool _isDead;

        public UnitBattleModel(int id, int level, int xp, 
            CharacterAttributes attributes, int currentHp) :
            base(id, level, xp, attributes)
        {
            hp = new DynamicHealth(level, attributes.BaseHealth);
            ((DynamicHealth)hp).HealthChange(currentHp);
        }
        
        public void RewardEarned()
        {
            LevelDataLogic.AddXp(ServiceLocator.Instance.EndGameReward.GetRewardedExp());
            
        }
    }
}