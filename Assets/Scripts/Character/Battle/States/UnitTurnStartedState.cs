using Character.Battle.Controller;

namespace Character.Battle.States
{
    public class UnitTurnStartedState : UnitBaseState
    {
        public UnitTurnStartedState(UnitBattleController controller, CharacterAnimationController animController) : base(controller, animController) { }

        public override void EnterState()
        {
            Controller.Model.IsUnitsTurn = true;
            Controller.SetInteractable(true);
            AnimController.PlayAnimation("Activated");
        }

        public override void ExitState() { }
    }
}
