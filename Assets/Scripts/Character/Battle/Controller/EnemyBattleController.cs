using Battle.StateMachine;
using Character.Base;
using Character.Battle.Model;

namespace Character.Battle.Controller
{
    public class EnemyBattleController : UnitBattleController
    {
        public EnemyBattleController(UnitView view, UnitBattleModel model, IBattleStateMachine battleStateMachine) : base(view, model, battleStateMachine) { }

        public override void SetTurnStatus(bool isUnitsTurn)
        {
            Model.IsUnitsTurn = isUnitsTurn;
            
            // Enemy attacks immediately when turn starts
            if (isUnitsTurn)
            {
                Attack();
            }
            else
            {
                SwitchState(TurnEndedState);
            }
        }
    }
}
