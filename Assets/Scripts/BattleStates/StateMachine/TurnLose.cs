using Character.Battle.Controller;

namespace BattleStates.StateMachine
{
    public class TurnLose : TurnBaseState
    {
        public TurnLose(BattleStateMachine battleStateMachine, BattleStateFactory factory) 
            : base(battleStateMachine, factory)
        {
        }

        public override void EnterState()
        {
            BattleStateMachine.PlayerControllers.ForEach((player) =>
                player.SetInteractable(false));
            BattleStateMachine.EnemyControllers.ForEach((enemy) =>
                enemy.SetInteractable(false));

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