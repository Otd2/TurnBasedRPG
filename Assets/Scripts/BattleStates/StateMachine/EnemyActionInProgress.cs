namespace BattleStates.StateMachine
{
    public class EnemyActionInProgress : TurnStateBase
    {
        public EnemyActionInProgress(BattleStateMachine battleStateMachine) : base(battleStateMachine)
        {
        }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
        }

        public override void SwitchToNextTurn()
        {
            BattleStateMachine.SwitchState(BattleStateMachine.PlayerTurnState);
        }
    }
}