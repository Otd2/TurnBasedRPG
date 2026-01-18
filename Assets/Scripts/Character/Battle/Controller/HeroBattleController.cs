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
        private HeroView HeroView => (HeroView)UnitView;
        private HeroBattleModel HeroModel => (HeroBattleModel)Model;

        public override void SetTurnStatus(bool isUnitsTurn)
        {
            if (Model.IsDead)
                return;
            
            base.SetTurnStatus(isUnitsTurn);
            HeroView.SetInteractable(Model.IsUnitsTurn);
        }

        public override void BattleEnd()
        {
            if (!IsDead)
            {
                HeroModel.RewardEarned();
            }
        }

        public void ShowInfoPopup()
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(UnitView.transform.position);
            Fire(EventNames.ShowInfoPopup, new ShowInfoPopupEvent(Model, screenPos));
        }

        public HeroBattleController(UnitView view, UnitBattleModel model, IBattleStateMachine battleStateMachine) : base(view, model, battleStateMachine)
        {
        }
    }
}
