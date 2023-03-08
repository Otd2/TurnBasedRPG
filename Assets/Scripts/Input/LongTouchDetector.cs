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

    private bool _isTouching = false;
    private float _touchTime = 0.0f;
    
    async UniTask WaitForTouchRelease()
    {
        while (_touchTime < longTouchDuration)
        {
            _touchTime += Time.deltaTime;
            if (!isInteractable)
                return;

            if (!_isTouching)
            {
                OnShortTouch?.Invoke();
                return;
            }
            await UniTask.Yield();
        }
        
        _touchTime = 0.0f;
        OnLongTouch?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isInteractable)
            return;
        
        _touchTime = 0.0f;
        _isTouching = true;
        OnTouchStarted?.Invoke();
        WaitForTouchRelease();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isTouching)
            return;
        
        _isTouching = false;
        _touchTime = 0.0f;
    }
}