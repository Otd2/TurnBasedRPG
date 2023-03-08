using Character.Battle.Controller;

namespace BattleStates.StateMachine
{
    public class PlayerActionInProgress : TurnBaseState
    {
        public PlayerActionInProgress(BattleStateMachine battleStateMachine, BattleStateFactory factory) 
            : base(battleStateMachine, factory)
        {
        }

        public override void EnterState()
        {
            BattleStateMachine.PlayerControllers.ForEach((player) => 
                player.SetInteractable(false));
        }

        public override void ExitState()
        {
        }

        public override void SwitchToNextTurn()
        {
            SwitchState(Factory.EnemyTurn);
        }
    }
}