using UnityEngine;

namespace InfoPopupController
{
    public abstract class PopupController : MonoBehaviour, IPopupController
    {
        [SerializeField] private RectTransform popupPanel; 
        public virtual void Show()
        {
            popupPanel.gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            popupPanel.gameObject.SetActive(false);
        }
    }
}