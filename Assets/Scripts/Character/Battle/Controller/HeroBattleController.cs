using Battle.StateMachine;
using Character.Base;
using Character.Battle.Model;
using Character.Battle.View;
using Events;
using UnityEngine;

namespace Character.Battle.Controller
{
    public class HeroBattleController : UnitBattleController
    {
        public override void SetTurnStatus(bool isUnitsTurn)
        {
            if (Model.IsDead)
                return;
            
            base.SetTurnStatus(isUnitsTurn);
            ((HeroView)UnitView).SetInteractable(Model.IsUnitsTurn);
        }

        public void BattleEnd()
        {
            if (!IsDead)
            {
                ((HeroBattleModel)Model).RewardEarned();
            }
        }

        public void ShowInfoPopup()
        {
            Vector3 screenPos = Camera.current.WorldToScreenPoint(UnitView.transform.position);
            EventBus.Publish(EventNames.ShowInfoPopup, new ShowInfoPopupEvent(Model, screenPos));
        }

        public HeroBattleController(UnitView view, UnitBattleModel model, IBattleStateMachine battleStateMachine) : base(view, model, battleStateMachine)
        {
        }
    }
}
