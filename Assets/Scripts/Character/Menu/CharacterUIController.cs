using System;
using Character.Base;

namespace Character.Menu
{
    public class CharacterUIController : CharacterController
    {
        public static event Action<int> OnAnyCharacterSelected = delegate(int id) {  }; 
        public static event Action<int> OnAnyCharacterUnselected = delegate(int id) {  }; 
        
        private readonly UnitUIModel _model;
        private CharacterUIView _view;
        
        public CharacterUIController(UnitView view, UnitUIModel model) : base(view, model)
        {
            _model = model;
            AssignView(view);
        }

        protected override void AssignView(UnitView view)
        {
            _view = (CharacterUIView)view;
            _view.SetController(this);
            _view.SetFrameActive(_model.IsSelected);
            _view.SetSprite(_model.Attributes.Sprite);
        } 
        public void ShowInfoPopup()
        {
            ServiceLocator.Instance.InfoPopupController.SetData
                (_model, _view.transform.position);
        }

        public void OnClickedHero()
        {
            Select(!_model.IsSelected);
        }

        private void Select(bool isSelected)
        {
            if(isSelected)
                _model.Select();
            else
                _model.Deselect();
            
            _view.SetFrameActive(_model.IsSelected);
            
            if(IsModelSelectionChanged(_model.IsSelected, isSelected))
                NotifySelectionChange();
        }

        private bool IsModelSelectionChanged(bool modelValue, bool givenValue)
        {
            return modelValue == givenValue;
        }

        private void NotifySelectionChange()
        {
            if (_model.IsSelected)
            {
                OnAnyCharacterSelected.Invoke(_model.Id);
            }
            else
            {
                OnAnyCharacterUnselected.Invoke(_model.Id);
            }
        }

        public override void Destroy()
        {
            _view.Destroy();
        }
    }
}