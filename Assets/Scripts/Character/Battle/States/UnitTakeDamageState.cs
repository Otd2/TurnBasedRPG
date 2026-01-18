using Character.Battle.Controller;

namespace Character.Battle.States
{
    // Just plays hit animation, HP logic is in Controller
    public class UnitTakeDamageState : UnitBaseState
    {
        public UnitTakeDamageState(UnitBattleController controller, CharacterAnimationController animController) : base(controller, animController) { }

        public override void EnterState()
        {
            AnimController.PlayAnimation("Hit");
        }

        public override void ExitState() { }
    }
}
