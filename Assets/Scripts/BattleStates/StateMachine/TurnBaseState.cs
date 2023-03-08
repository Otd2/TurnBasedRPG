namespace BattleStates.StateMachine
{
    public abstract class TurnBaseState
    {
        protected readonly BattleStateMachine BattleStateMachine;
        protected readonly BattleStateFactory Factory;

        public TurnBaseState(BattleStateMachine battleStateMachine, BattleStateFactory factory)
        {
            BattleStateMachine = battleStateMachine;
            Factory = factory;
        }
        public abstract void EnterState();
        public abstract void ExitState();

        public virtual void SwitchState(TurnBaseState turnBaseState)
        {
            ExitState();
            BattleStateMachine.CurrentState = turnBaseState;
            turnBaseState.EnterState();
        }

        public abstract void SwitchToNextTurn();
    }
}