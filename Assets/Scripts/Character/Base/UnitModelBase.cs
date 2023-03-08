using HitPoint;
using Level;
using PersistentData;

namespace Character.Base
{
    public class UnitModelBase
    {
        protected readonly PersistantDataManager PersistentDataManager;
        private int _attackPower;
        public int Id { get; }

        private readonly LevelData _levelData;
        public LevelData LevelDataLogic => _levelData;
        
        protected Health hp;
        public Health Hp => hp;
        
        public int AttackPower
        {
            get => _attackPower;
        }

        public CharacterAttributes Attributes { get; }

        protected UnitModelBase(int id, int level, int xp, 
            CharacterAttributes attributes,
            PersistantDataManager persistentDataManager)
        {
            PersistentDataManager = persistentDataManager;
            Id = id;
            _levelData = new LevelData(xp, level);
            Attributes = attributes;
            hp = new Health(level, attributes.BaseHealth);
            CalculateAttackPower();
        }

        private void CalculateAttackPower()
        {
            _attackPower = ServiceLocator.Instance.AttackPowerLogicService
                .GetAttackValue(Attributes.BaseAttackPower, _levelData.Level);
        }
        
    }
}