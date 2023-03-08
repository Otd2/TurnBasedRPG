using Character.Battle.Controller;

namespace BattleStates.StateMachine
{
    public class EnemyActionInProgress : TurnBaseState
    {
        public EnemyActionInProgress(BattleStateMachine battleStateMachine, BattleStateFactory factory) 
            : base(battleStateMachine, factory)
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
            SwitchState(Factory.PlayerTurn);
        }
    }
}