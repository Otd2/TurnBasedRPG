namespace Battle.StateMachine
{
    public class PlayerActionInProgress : TurnStateBase
    {
        public PlayerActionInProgress(BattleStateMachine battleStateMachine) : base(battleStateMachine)
        {
        }

        public override void EnterState()
        {
            BattleStateMachine.PlayerControllers.ForEach((player) => player.SetInteractable(false));
        }

        public override void ExitState()
        {
        }

        public override void SwitchToNextTurn()
        {
            BattleStateMachine.SwitchState(BattleStateMachine.EnemyTurnState);
        }
    }
}