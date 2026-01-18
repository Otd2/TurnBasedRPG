using System.Collections.Generic;
using Character.Battle.Controller;

namespace BattleStates.StateMachine
{
    public class PlayerTurnState : TurnStateBase
    {
        public PlayerTurnState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
        {
        }

        public override void EnterState()
        {
            if (CheckIfAllCharacterDead(BattleStateMachine.PlayerControllers))
            {
                BattleStateMachine.SwitchState(BattleStateMachine.LoseState);
                return;
            }
        
            BattleStateMachine.UI.ShowPlayerTurnUI();
        
            BattleStateMachine.PlayerControllers.ForEach((player) => player.SetTurnStatus(true));
            
            BattleStateMachine.EnemyControllers.ForEach((player) => player.SetTurnStatus(false));
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
            BattleStateMachine.SwitchState(BattleStateMachine.PlayerActionState);
        }
    }
}