using Character.Battle.Controller;
using UnityEngine;

namespace Character.Battle.States
{
    public class UnitAttackingState : UnitBaseState
    {
        public override void EnterState()
        {
            UnitController.AttackStrategy.Execute(UnitController.Model.AttackPower);
            CharacterAnimationController.PlayAnimation("Attack");
        }

        public override void ExitState()
        {
        }

        public UnitAttackingState(UnitBattleController unitController, 
            CharacterAnimationController characterAnimationController, UnitStateFactory factory) :
            base(unitController, characterAnimationController, factory)
        {
        }
    }
}