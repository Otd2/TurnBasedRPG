using System.Collections.Generic;
using Character.Battle.Controller;

namespace BattleStates.StateMachine
{
    public class BattleStateMachine : IBattleStateMachine
    {
        public List<UnitBattleController> EnemyControllers { get; private set; }
        public List<UnitBattleController> PlayerControllers { get; private set; }
        public BattleUI UI { get; }
        public BattleBoardController BoardController { get; }
        public TurnStateBase Current { get; private set; }

        // Cached states
        public readonly PlayerTurnState PlayerTurnState;
        public readonly EnemyTurnState EnemyTurnState;
        public readonly PlayerActionInProgress PlayerActionState;
        public readonly EnemyActionInProgress EnemyActionState;
        public readonly TurnStateWin StateWinState;
        public readonly TurnStateLose StateLoseState;

        public BattleStateMachine(BattleBoardController battleBoardController, BattleUI battleUI)
        {
            BoardController = battleBoardController;
            UI = battleUI;

            // Initialize all states once
            PlayerTurnState = new PlayerTurnState(this);
            EnemyTurnState = new EnemyTurnState(this);
            PlayerActionState = new PlayerActionInProgress(this);
            EnemyActionState = new EnemyActionInProgress(this);
            StateWinState = new TurnStateWin(this);
            StateLoseState = new TurnStateLose(this);
        }

        public void InitBattle(List<UnitBattleController> playerControllers, List<UnitBattleController> enemyControllers)
        {
            PlayerControllers = playerControllers;
            EnemyControllers = enemyControllers;
            Current = PlayerTurnState;
            Current.EnterState();
        }

        public void SwitchState(TurnStateBase @new)
        {
            Current.ExitState();
            Current = @new;
            @new.EnterState();
        }

        public void TurnActionStarted()
        {
            Current.SwitchToNextTurn();
        }

        public void TurnEnded()
        {
            Current.SwitchToNextTurn();
        }
    }
}