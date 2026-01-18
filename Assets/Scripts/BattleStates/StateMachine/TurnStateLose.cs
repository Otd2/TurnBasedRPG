namespace BattleStates.StateMachine
{
    public class TurnStateLose : TurnStateBase
    {
        public TurnStateLose(BattleStateMachine battleStateMachine) : base(battleStateMachine)
        {
        }

        public override void EnterState()
        {
            BattleStateMachine.PlayerControllers.ForEach((player) => player.SetInteractable(false));
            BattleStateMachine.EnemyControllers.ForEach((enemy) => enemy.SetInteractable(false));

            BattleStateMachine.BoardController.ClearBattleData();
        
            BattleStateMachine.UI.ShowLoseUI();
        }

        public override void ExitState()
        {
        }

        public override void SwitchToNextTurn()
        {
        }
    }
}