using Character.Battle.Controller;
using UnityEngine;

namespace Character.Battle.States
{
    public class UnitTurnStartedState: UnitBaseState
    {
        public override void EnterState()
        {
            UnitController.Model.IsUnitsTurn = true;
            UnitController.SetInteractable(true);
            CharacterAnimationController.PlayAnimation("Activated");
        }

        public override void ExitState()
        {
        }

        public UnitTurnStartedState(UnitBattleController unitController, CharacterAnimationController characterAnimationController, UnitStateFactory factory) : base(unitController, characterAnimationController, factory)
        {
        }
    }
}