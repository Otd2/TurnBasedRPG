namespace BattleStates.StateMachine
{
    public class BattleStateFactory
    {
        public TurnBaseState PlayerTurn => new PlayerTurn(_battleStateMachine, this);
        public TurnBaseState EnemyTurn => new EnemyTurn(_battleStateMachine, this);
        public TurnBaseState PlayerActionInProgress => new PlayerActionInProgress(_battleStateMachine, this);
        public TurnBaseState EnemyActionInProgress => new EnemyActionInProgress(_battleStateMachine, this);
        public TurnBaseState Win => new TurnWin(_battleStateMachine, this);
        public TurnBaseState Lose => new TurnLose(_battleStateMachine, this);

        private readonly BattleStateMachine _battleStateMachine;
        public BattleStateFactory(BattleStateMachine battleStateMachine)
        {
            _battleStateMachine = battleStateMachine;
        }

    }
}