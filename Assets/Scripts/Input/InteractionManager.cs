using System;
using UnityEngine;

namespace Input
{
    public class InteractionManager
    {
        private LongTouchDetector _longTouchDetector;
        
        public void AssignLongTouchDetector(GameObject gameObject, Action onLongTouch, Action onShortTouch, Action onTouchStarted)
        {
            _longTouchDetector = gameObject.AddComponent<LongTouchDetector>();
            if (onLongTouch != null) _longTouchDetector.OnLongTouch += onLongTouch;
            if (onShortTouch != null) _longTouchDetector.OnShortTouch += onShortTouch;
            if (onTouchStarted != null) _longTouchDetector.OnTouchStarted += onTouchStarted;
        }
        
        public void SetInteractable(bool isInteractable)
        {
            _longTouchDetector.isInteractable = isInteractable;
        }

        
    }
}