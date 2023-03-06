using Character;
using DefaultNamespace.Health;
using DG.Tweening;
using Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Character
{
    public class HeroView : BattleUnitView
    {
        private InteractionManager interactionManager;
        public override void SetController(CharacterController controller)
        {
            base.SetController(controller);
            interactionManager =
                new InteractionManager();
            interactionManager.AssignLongTouchDetector(gameObject, OnLongTouch, OnShortTouch, OnTouchStarted);
        }

        public void SetInteractable(bool isActive)
        {
            interactionManager.SetInteractable(isActive);
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        private void OnTouchStarted()
        {
            //Set Animation
        }

        private void OnLongTouch()
        {
            ((HeroBattleController)_controller).ShowInfoPopup();
        }

        private void OnShortTouch()
        {
            //Interraction
            
            ((HeroBattleController)_controller).Attack();
        }
    }
    
}

