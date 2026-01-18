using Combat.Services;
using Core;
using Health;
using Level;
using Persistence;

namespace Character.Base
{
    public class UnitModelBase
    {
        protected readonly PersistentDataManager PersistentDataManager;
        private int _attackPower;
        public int Id { get; }

        private readonly LevelData _levelData;
        public LevelData LevelDataLogic => _levelData;
        
        protected HealthPoints hp;
        public HealthPoints Hp => hp;
        
        public int AttackPower
        {
            get => _attackPower;
        }

        public CharacterAttributes Attributes { get; }

        protected UnitModelBase(int id, int level, int xp, CharacterAttributes attributes, PersistentDataManager persistentDataManager)
        {
            PersistentDataManager = persistentDataManager;
            Id = id;
            _levelData = new LevelData(xp, level);
            Attributes = attributes;
            hp = new HealthPoints(level, attributes.BaseHealth);
            CalculateAttackPower();
        }

        private void CalculateAttackPower()
        {
            _attackPower = ServiceLocator.Instance.Get<IAttackPowerLogicService>().GetAttackValue(Attributes.BaseAttackPower, _levelData.Level);
        }
        
    }
}