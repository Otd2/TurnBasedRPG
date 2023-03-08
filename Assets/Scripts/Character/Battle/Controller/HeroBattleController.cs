using BattleStates;
using BattleStates.StateMachine;
using Character.Base;
using Character.Battle.Model;
using Character.Battle.View;
using DefaultNamespace;
using PersistentData;
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
            ServiceLocator.Instance.InfoPopupController.SetData
                (Model, Camera.current.WorldToScreenPoint(UnitView.transform.position));
        }

        public HeroBattleController(UnitView view, UnitBattleModel model,
            IBattleStateMachine battleStateMachine) : base(view, model, battleStateMachine)
        {
        }
    }

}