using Character.Battle.Controller;

namespace BattleStates.StateMachine
{
    public class TurnStateWin : TurnStateBase
    {
        public TurnStateWin(BattleStateMachine battleStateMachine) : base(battleStateMachine)
        {
        }

        public override void EnterState()
        {
            BattleStateMachine.PlayerControllers.ForEach((player) => player.SetInteractable(false));
            BattleStateMachine.EnemyControllers.ForEach((enemy) => enemy.SetInteractable(false));
        
            BattleStateMachine.BoardController.ClearBattleData();
        
            foreach (var playerController in BattleStateMachine.PlayerControllers)
            {
                ((HeroBattleController)playerController).BattleEnd();
            }
        
            BattleStateMachine.UI.ShowWinUI();
        }

        public override void ExitState()
        {
        }

        public override void SwitchToNextTurn()
        {
        }
    }
}