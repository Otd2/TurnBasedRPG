using System;
using DefaultNamespace;
using UnityEngine;

namespace Character
{
    public class CharacterUIController : CharacterController
    {
        private readonly UnitUIModel _model;
        private CharacterUIView _view;
        public bool IsUnlocked => _model._isUnlocked; 
        
        
        public static event Action<int> OnAnyCharacerSelected = delegate(int id) {  }; 
        public static event Action<int> OnAnyCharacterUnselected = delegate(int id) {  }; 
        
        public CharacterUIController(UnitView view, UnitUIModel model, 
            PlayerPrefsPersistentDataManager playerPrefsPersistentDataManager) : base(view, model, playerPrefsPersistentDataManager)
        {
            _model = model;
            AssignView(view);
        }

        protected override void AssignView(UnitView view)
        {
            _view = (CharacterUIView)view;
            _view.SetController(this);
            _view.SetFrameActive(_model._isSelected);
            _view.SetSprite(_model.Attributes.Sprite);
            _view.SetUnlocked(_model._isUnlocked);
        } 
        public void ShowInfoPopup()
        {
            ServiceLocator.Instance.InfoPopupController.SetData
                (_model, _view.transform.position);
        }

        public void OnClickedHero()
        {
            if (!_model._isUnlocked)
                return;
            
            Select(!_model._isSelected);
        }

        private void Select(bool isSelected)
        {
            if (PlayerPrefsPersistentDataManager.CurrentGameData.selectedHeroes.Count >= 3 && isSelected)
                return;
            
            _model._isSelected = isSelected;
            _view.SetFrameActive(isSelected);
            if (isSelected)
            {
                
                PlayerPrefsPersistentDataManager.CurrentGameData.selectedHeroes.Add(_model.Id);
                OnAnyCharacerSelected(_model.Id);
            }
            else
            {
                PlayerPrefsPersistentDataManager.CurrentGameData.selectedHeroes.Remove(_model.Id);
                OnAnyCharacterUnselected(_model.Id);
            }
        }
        
        public void Unlocked()
        {
            _model._isUnlocked = true;
            PlayerPrefsPersistentDataManager.OnCharacterUnlocked(_model.Id);
        }

        public override void Destroy()
        {
            _view.Destroy();
        }
    }
}