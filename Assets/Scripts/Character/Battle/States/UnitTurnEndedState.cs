using Character.Battle.Controller;
using Character.Battle.States;
using UnityEngine;

namespace Character
{
    public class UnitTurnEndedState: UnitBaseState
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