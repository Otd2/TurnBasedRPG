using PersistentData;

namespace Character.Battle.Model
{
    public class EnemyBattleModel : UnitBattleModel 
    {
        protected override void OnHealthChanged(int newhealth)
        {
            PersistentDataManager.GetBattleData().enemyHp = newhealth;
        }

        public EnemyBattleModel(int id, int level, int xp, CharacterAttributes attributes, PersistantDataManager persistentDataManager) : base(id, level, xp, attributes, persistentDataManager)
        {
            hp.HealthChange(PersistentDataManager.GetBattleData().enemyHp);
        }
    }
}