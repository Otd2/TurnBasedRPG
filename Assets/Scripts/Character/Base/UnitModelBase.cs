using System.Runtime.Serialization;
using DefaultNamespace;
using DefaultNamespace.Health;
using Level;
using UnityEngine;

namespace Character
{
    public class UnitModelBase
    {
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

        protected UnitModelBase(int id, int level, int xp, CharacterAttributes attributes)
        {
            Id = id;
            _levelData = new LevelData(xp, level);
            this.Attributes = attributes;
            CalculateAttackPower();
        }


        private void CalculateAttackPower()
        {
            _attackPower = ServiceLocator.Instance.AttackIncreaseLogicService
                .GetAttackValue(Attributes.BaseAttackPower, _levelData.Level);
        }
        
    }
}