using Character.Battle.Controller;

namespace Character.Battle.States
{
    public class UnitAttackingState : UnitBaseState
    {
        public UnitAttackingState(UnitBattleController controller, CharacterAnimationController animController) : base(controller, animController) { }

        public override void EnterState()
        {
            Controller.AttackCommand.Execute();
            AnimController.PlayAnimation("Attack");
        }

        public override void ExitState() { }
    }
}
