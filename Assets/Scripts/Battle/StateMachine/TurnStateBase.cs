using Events;

namespace Battle.StateMachine
{
    public abstract class TurnStateBase : EventLayer
    {
        protected readonly BattleStateMachine BattleStateMachine;

        protected TurnStateBase(BattleStateMachine battleStateMachine)
        {
            BattleStateMachine = battleStateMachine;
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void SwitchToNextTurn();
    }
}