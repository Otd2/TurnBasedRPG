using Character.Battle.Controller;

namespace Character.Battle.States
{
    public class UnitDiedState : UnitBaseState
    {
        public override void EnterState()
        {
            CharacterAnimationController.PlayAnimation("Died");
        }

        public override void ExitState()
        {
        }


        public UnitDiedState(UnitBattleController unitController, CharacterAnimationController characterAnimationController, UnitStateFactory factory) : base(unitController, characterAnimationController, factory)
        {
        }
    }
}