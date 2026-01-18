using Character.Base;
using Persistence;

namespace Character.Battle.Model
{
    public class UnitBattleModel : UnitModelBase 
    {
        public bool IsDead => hp.GetHp() <= 0;
        public bool IsUnitsTurn { get; set; }

        protected UnitBattleModel(int id, int level, int xp, CharacterAttributes attributes, PersistentDataManager persistentDataManager) : base(id, level, xp, attributes, persistentDataManager)
        {
            //update persistent data when health changed
            hp.OnHealthChanged += OnHealthChanged;
        }
        
        protected virtual void OnHealthChanged(int newhealth)
        {
            PersistentDataManager.OnCharacterHPChanged(Id, newhealth);
        }
    }
}