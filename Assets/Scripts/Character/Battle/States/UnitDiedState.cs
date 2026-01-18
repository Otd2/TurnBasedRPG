using Character.Battle.Controller;

namespace Character.Battle.States
{
    public class UnitDiedState : UnitBaseState
    {
        public UnitDiedState(UnitBattleController controller, CharacterAnimationController animController) : base(controller, animController) { }

        public override void EnterState()
        {
            AnimController.PlayAnimation("Died");
        }

        public override void ExitState() { }
    }
}
