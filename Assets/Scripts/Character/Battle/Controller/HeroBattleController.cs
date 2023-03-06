using DefaultNamespace;
using UnityEngine;

namespace Character
{
    public class HeroBattleController : CharacterBattleController
    {
        public override void Destroy()
        {
            base.Destroy();
        }

        public override void SetCharacterState(CharacterBattleState battleState)
        {
            base.SetCharacterState(battleState);
            ((HeroView)_unitView).SetInteractable(isActive);
        }

        public void MatchEnded()
        {
            if (!IsDead)
            {
                _model.RewardEarned();
                PlayerPrefsPersistentDataManager.GetCharacterData(_model.Id).Exp = _model.LevelDataLogic.Xp;
                PlayerPrefsPersistentDataManager.GetCharacterData(_model.Id).Lvl = _model.LevelDataLogic.Level;
            }
        }

        public void ShowInfoPopup()
        {
            ServiceLocator.Instance.InfoPopupController.SetData
                (_model, Camera.current.WorldToScreenPoint(_unitView.transform.position));
        }

        public HeroBattleController(UnitView view, UnitBattleModel model, PlayerPrefsPersistentDataManager playerPrefsPersistentDataManager,
            ITurnManager turnManager) : base(view, model, playerPrefsPersistentDataManager, turnManager)
        {
        }
    }

}