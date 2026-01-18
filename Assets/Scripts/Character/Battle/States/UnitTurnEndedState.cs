using Character.Battle.Controller;

namespace Character.Battle.States
{
    public class UnitTurnEndedState : UnitBaseState
    {
        public UnitTurnEndedState(UnitBattleController controller, CharacterAnimationController animController) : base(controller, animController) { }

        public override void EnterState()
        {
            Controller.Model.IsUnitsTurn = false;
            AnimController.PlayAnimation("Deactivated");
        }

        public override void ExitState() { }
    }
}
