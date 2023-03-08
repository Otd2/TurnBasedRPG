using Character.Battle.Controller;

namespace Character.Battle.States
{
    public abstract class UnitBaseState
    {
        protected readonly UnitBattleController UnitController;
        protected readonly CharacterAnimationController CharacterAnimationController;
        protected readonly UnitStateFactory Factory;

        public UnitBaseState(UnitBattleController unitController,
            CharacterAnimationController characterAnimationController,
            UnitStateFactory factory)
        {
            UnitController = unitController;
            CharacterAnimationController = characterAnimationController;
            Factory = factory;
        }
        public abstract void EnterState();
        public abstract void ExitState();

        public virtual void SwitchState(UnitBaseState unitBaseState)
        {
            ExitState();
            unitBaseState.EnterState();
            UnitController.CurrentState = unitBaseState;
        }
    }
}