using BattleStates.StateMachine;
using Character.Base;
using Character.Battle.Model;
using Character.Battle.View;

namespace Character.Battle.Controller
{
    public class EnemyBattleController : UnitBattleController
    {
        private BattleUnitView _view;
        
        public override void SetTurnStatus(bool isUnitsTurn)
        {
            Model.IsUnitsTurn = isUnitsTurn;
            //switch to attack state when its turn
            CurrentState.SwitchState(isUnitsTurn ? Factory.AttackingSate : Factory.TurnEndedState);
        }

        public EnemyBattleController(UnitView view, UnitBattleModel model, IBattleStateMachine battleStateMachine) : base(view, model, battleStateMachine)
        {
        }
    }
}