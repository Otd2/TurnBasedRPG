using Character.Battle.Controller;
using Input;
using CharacterController = Character.Base.CharacterController;

namespace Character.Battle.View
{
    public class HeroView : BattleUnitView
    {
        private HeroBattleController HeroController => (HeroBattleController)Controller;
        private InteractionManager interactionManager;

        public override void SetController(CharacterController controller)
        {
            base.SetController(controller);
            
            interactionManager = new InteractionManager();
            interactionManager.AssignLongTouchDetector(gameObject, OnLongTouch, OnShortTouch, null);
        }

        public void SetInteractable(bool isActive)
        {
            interactionManager.SetInteractable(isActive);
        }

        private void OnLongTouch()
        {
            HeroController.ShowInfoPopup();
        }

        private void OnShortTouch()
        {
            HeroController.Attack();
        }
    }
    
}

