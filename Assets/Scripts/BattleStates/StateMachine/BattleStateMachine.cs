using System.Collections.Generic;
using Character.Battle.Controller;
using UnityEngine;

namespace BattleStates.StateMachine
{
    public class BattleStateMachine : IBattleStateMachine
    {
        #region Fields

        private readonly BattleStateFactory _battleStateFactory;

        #endregion

        #region Properties
        public List<UnitBattleController> EnemyControllers { get; private set; }
        public List<UnitBattleController> PlayerControllers { get; private set; }
        public BattleUI UI { get; }
        public BattleBoardController BoardController { get; }
        public TurnBaseState CurrentState { set; get; }

        #endregion

        public BattleStateMachine(BattleBoardController battleBoardController, BattleUI battleUI)
        {
            BoardController = battleBoardController;
            UI = battleUI;
            _battleStateFactory = new BattleStateFactory(this);
        }

        public void InitBattle(List<UnitBattleController> playerControllers, List<UnitBattleController> enemyControllers)
        {
            PlayerControllers = playerControllers;
            EnemyControllers = enemyControllers;
            CurrentState = _battleStateFactory.PlayerTurn;
            CurrentState.EnterState();
        }

        //Called by units to trigger next state
        public void TurnActionStarted()
        {
            CurrentState.SwitchToNextTurn();
        }

        public void TurnEnded()
        {
            CurrentState.SwitchToNextTurn();
        }
    }
}