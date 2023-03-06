using System;
using Input;
using UnityEngine;
using UnityEngine.UI;

namespace Character
{
    public class CharacterUIView : UnitView
    {
        [SerializeField] private Image characterSprite;
        [SerializeField] private Image frame;
        private InteractionManager interactionManager;

        private void Awake()
        {
            interactionManager =
                new InteractionManager();
            
            interactionManager.AssignLongTouchDetector(gameObject, OnLongTouch, OnShortTouch, OnTouchStarted);
        }

        public void SetFrameActive(bool isActive)
        {
            frame.gameObject.SetActive(isActive);
        }
        
        public void SetSprite(Sprite sprite)
        {
            characterSprite.sprite = sprite;
        }

        public void SetUnlocked(bool modelUnlocked)
        {
            characterSprite.color = modelUnlocked ? Color.white : Color.grey;
            interactionManager.SetInteractable(modelUnlocked);
        }

        private void OnTouchStarted()
        {
        }

        private void OnLongTouch()
        {
            ((CharacterUIController)_controller).ShowInfoPopup();
        }

        private void OnShortTouch()
        {
            ((CharacterUIController)_controller).OnClickedHero();
        }
    }
}