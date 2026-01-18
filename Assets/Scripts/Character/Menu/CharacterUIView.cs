using Character.Base;
using Input;
using UnityEngine;
using UnityEngine.UI;

namespace Character.Menu
{
    public class CharacterUIView : UnitView
    {
        [SerializeField] private Image characterSprite;
        [SerializeField] private Image frame;
        private InteractionManager _interactionManager;

        private void Awake()
        {
            _interactionManager =
                new InteractionManager();
            
            _interactionManager.AssignLongTouchDetector(gameObject, OnLongTouch, OnShortTouch, OnTouchStarted);
            _interactionManager.SetInteractable(true);
        }

        public void SetFrameActive(bool isActive)
        {
            frame.gameObject.SetActive(isActive);
        }
        
        public void SetSprite(Sprite sprite)
        {
            characterSprite.sprite = sprite;
        }
        
        private void OnTouchStarted()
        {
        }

        private void OnLongTouch()
        {
            ((CharacterUIController)Controller).ShowInfoPopup();
        }

        private void OnShortTouch()
        {
            ((CharacterUIController)Controller).OnClickedHero();
        }
    }
}