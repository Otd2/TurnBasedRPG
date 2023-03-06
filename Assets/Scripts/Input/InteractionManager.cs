using System;
using UnityEngine;

namespace Input
{
    public class InteractionManager
    {
        private LongTouchDetector longTouchDetector;
        
        public void AssignLongTouchDetector(GameObject gameObject, Action onLongTouch, Action onShortTouch, Action onTouchStarted)
        {
            longTouchDetector = gameObject.AddComponent<LongTouchDetector>();
            longTouchDetector.onLongTouch += onLongTouch;
            longTouchDetector.onShortTouch += onShortTouch;
            longTouchDetector.onTouchStarted += onTouchStarted;
        }
        
        public void SetInteractable(bool isInteractable)
        {
            longTouchDetector.IsInteractable = isInteractable;
        }

        
    }
}