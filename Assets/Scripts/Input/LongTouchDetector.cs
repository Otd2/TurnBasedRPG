using System;
using Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongTouchDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float longTouchDuration = 1.0f;
    public Action onLongTouch;
    public Action onTouchStarted;
    public Action onShortTouch;
    public bool IsInteractable;

    private bool isTouching = false;
    private float touchTime = 0.0f;
    
    public static bool IsInteracting;

    private void Update()
    {
        if (!IsInteractable)
            return;
        
        if (isTouching)
        {
            touchTime += Time.deltaTime;

            if (touchTime >= longTouchDuration)
            {
                onLongTouch?.Invoke();
                isTouching = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        touchTime = 0.0f;
        isTouching = true;
        onTouchStarted?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isTouching)
            return;
        
        isTouching = false;
        
        if(touchTime < longTouchDuration)
            onShortTouch?.Invoke();
        
        touchTime = 0.0f;
    }
}