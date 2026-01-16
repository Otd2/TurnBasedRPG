using Character.Base;
using Events;

namespace Character.Menu
{
    public class CharacterUIController : CharacterController
    {
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
            EventBus.Publish(EventNames.ShowInfoPopup, new ShowInfoPopupEvent(_model, _view.transform.position));
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
            var eventName = _model.IsSelected ? EventNames.CharacterSelected : EventNames.CharacterUnselected;
            EventBus.Publish(eventName, new CharacterSelectionEvent(_model.Id));
        }

        public override void Destroy()
        {
            _view.Destroy();
        }
    }
}
