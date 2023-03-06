using System;

namespace Character
{
    public abstract class InteractableUnitView : UnitView
    {
        protected LongTouchDetector longTouchDetector;
        
        protected void AssignLongTouchDetector()
        {
            longTouchDetector = gameObject.AddComponent<LongTouchDetector>();
            longTouchDetector.onLongTouch += OnLongTouch;
            longTouchDetector.onShortTouch += OnShortTouch;
            longTouchDetector.onTouchStarted += OnTouchStarted;
        }

        public override void SetController(CharacterController controller)
        {
            base.SetController(controller);
            AssignLongTouchDetector();
        }
        
        public void SetInteractable(bool isInteractable)
        {
            longTouchDetector.IsInteractable = isInteractable;
        }

        protected abstract void OnTouchStarted();
        protected abstract void OnLongTouch();
        protected abstract void OnShortTouch();
    }
}