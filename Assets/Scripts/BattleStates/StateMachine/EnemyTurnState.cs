using System.Collections.Generic;
using Character.Battle.Controller;
using Events;
using Events.Interfaces;

namespace BattleStates.StateMachine
{
    public class EnemyTurnState : TurnStateBase
    {
        public EnemyTurnState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
        {
        }

        public override void EnterState()
        {
            Subscribe(EventNames.Errors.NoTargetToAttack, OnNoTargetToAttackHandler);
            if (CheckIfAllCharacterDead(BattleStateMachine.EnemyControllers))
            {
                BattleStateMachine.SwitchState(BattleStateMachine.StateWinState);
                return;
            }

            BattleStateMachine.EnemyControllers.ForEach((enemy) => enemy.SetTurnStatus(true));
            BattleStateMachine.PlayerControllers.ForEach((enemy) => enemy.SetTurnStatus(false));
        }

        /// <summary>
        /// If there is not target to attack, switch to next turn.
        /// </summary>
        /// <param name="obj"></param>
        private void OnNoTargetToAttackHandler(IEvent obj)
        {
            SwitchToNextTurn();
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
            BattleStateMachine.SwitchState(BattleStateMachine.EnemyActionState);
        }
    }
}