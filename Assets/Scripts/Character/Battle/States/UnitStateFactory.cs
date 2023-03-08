using Character.Battle.Controller;
using Character.Battle.States;

namespace Character
{
    public class UnitStateFactory
    {
        public UnitBaseState TurnStartedState => new UnitTurnStartedState(_unitController, _characterAnimationController, this);
        public UnitBaseState AttackingSate => new UnitAttackingState(_unitController, _characterAnimationController, this);
        public UnitBaseState DiedState => new UnitDiedState(_unitController, _characterAnimationController, this);
        public UnitBaseState TurnEndedState => new UnitTurnEndedState(_unitController, _characterAnimationController, this);
        
        private UnitBattleController _unitController;
        private readonly CharacterAnimationController _characterAnimationController;

        public UnitStateFactory(UnitBattleController unitController, CharacterAnimationController characterAnimationController)
        {
            _unitController = unitController;
            _characterAnimationController = characterAnimationController;
        }

        public UnitBaseState CreateTakeDamageState(int damage)
        {
            return new UnitTakeDamageState(_unitController, _characterAnimationController, this,damage);
        }
    }
        


    
}