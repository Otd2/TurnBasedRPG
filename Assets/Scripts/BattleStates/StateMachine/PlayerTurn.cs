using System.Collections.Generic;
using Character.Battle.Controller;

namespace BattleStates.StateMachine
{
    public class PlayerTurn : TurnBaseState
    {
        public PlayerTurn(BattleStateMachine battleStateMachine, BattleStateFactory factory) 
            : base(battleStateMachine, factory)
        {
        }

        public override void EnterState()
        {
            if (CheckIfAllCharacterDead(BattleStateMachine.PlayerControllers))
            {
                SwitchState(Factory.Lose);
                return;
            }
        
            BattleStateMachine.UI.ShowPlayerTurnUI();
        
            BattleStateMachine.PlayerControllers.ForEach((player) => 
                player.SetTurnStatus(true));
            
            BattleStateMachine.EnemyControllers.ForEach((player) => 
                player.SetTurnStatus(false));
        }

        public override void ExitState()
        {
            BattleStateMachine.UI.HidePlayerTurnUI();
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
            SwitchState(Factory.PlayerActionInProgress);
        }
    }
}