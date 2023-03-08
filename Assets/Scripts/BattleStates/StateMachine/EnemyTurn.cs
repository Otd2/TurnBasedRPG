using System.Collections.Generic;
using Character.Battle.Controller;
using UnityEngine;

namespace BattleStates.StateMachine
{
    public class EnemyTurn : TurnBaseState
    {
        public EnemyTurn(BattleStateMachine battleStateMachine, BattleStateFactory factory) 
            : base(battleStateMachine, factory)
        {
        }

        public override void EnterState()
        {
            if (CheckIfAllCharacterDead(BattleStateMachine.EnemyControllers))
            {
                SwitchState(Factory.Win);
                return;
            
            }
            BattleStateMachine.EnemyControllers.ForEach((enemy) => 
                enemy.SetTurnStatus(true));
            
            BattleStateMachine.PlayerControllers.ForEach((enemy) => 
                enemy.SetTurnStatus(false));
        }

        public override void ExitState()
        {
        }

        private bool CheckIfAllCharacterDead(List<UnitBattleController> battleCharacters)
        {
            foreach (var battleController in battleCharacters)
            {
                if (!battleController.IsDead)
                    return false;
            }

            return true;
        }

        public override void SwitchToNextTurn()
        {
            SwitchState(Factory.EnemyActionInProgress);
        }
    }
}