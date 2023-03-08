using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class LongTouchDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float longTouchDuration = 1.0f;
    public Action OnLongTouch;
    public Action OnTouchStarted;
    public Action OnShortTouch;
    public bool isInteractable;

    private bool isTouching = false;
    private float touchTime = 0.0f;
    
    async UniTask WaitForTouchRelease()
    {
        while (touchTime < longTouchDuration)
        {
            touchTime += Time.deltaTime;
            if (!isInteractable)
                return;

            if (!isTouching)
            {
                OnShortTouch?.Invoke();
                return;
            }
            await UniTask.Yield();
        }
        
        touchTime = 0.0f;
        OnLongTouch?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isInteractable)
            return;
        
        touchTime = 0.0f;
        isTouching = true;
        OnTouchStarted?.Invoke();
        WaitForTouchRelease();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isTouching)
            return;
        
        isTouching = false;
        touchTime = 0.0f;
    }
}