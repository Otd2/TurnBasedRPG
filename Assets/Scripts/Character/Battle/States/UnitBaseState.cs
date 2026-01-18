using Character.Battle.Controller;

namespace Character.Battle.States
{
    public abstract class UnitBaseState
    {
        protected readonly UnitBattleController Controller;
        protected readonly CharacterAnimationController AnimController;

        protected UnitBaseState(UnitBattleController controller, CharacterAnimationController animController)
        {
            Controller = controller;
            AnimController = animController;
        }
        
        public abstract void EnterState();
        public abstract void ExitState();
    }
}
