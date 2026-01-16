using Character.Battle.Controller;

namespace Character.Battle.States
{
    public class UnitTurnEndedState : UnitBaseState
    {
        public override void EnterState()
        {
            UnitController.Model.IsUnitsTurn = false;
            CharacterAnimationController.PlayAnimation("Deactivated");
        }

        public override void ExitState()
        {
        }


        public UnitTurnEndedState(UnitBattleController unitController, CharacterAnimationController characterAnimationController, UnitStateFactory factory) : base(unitController, characterAnimationController, factory)
        {
        }
    }
}