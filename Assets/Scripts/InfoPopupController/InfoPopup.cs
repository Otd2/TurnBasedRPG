using Character.Base;
using Events;
using Events.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InfoPopupController
{
    public class InfoPopup : PopupController
    {
        [SerializeField] private TextMeshProUGUI txt_attackPower;
        [SerializeField] private TextMeshProUGUI txt_name;
        [SerializeField] private TextMeshProUGUI txt_exp;
        [SerializeField] private TextMeshProUGUI txt_level;
        [SerializeField] private RectTransform infoPopup;
        [SerializeField] private Button closeButton;

        private void Awake()
        {
            closeButton.onClick.AddListener(Hide);
        }

        private void OnEnable()
        {
            EventBus.Subscribe(EventNames.ShowInfoPopup, OnShowInfoPopup);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(EventNames.ShowInfoPopup, OnShowInfoPopup);
        }

        private void OnDestroy()
        {
            closeButton.onClick.RemoveAllListeners();
        }

        private void OnShowInfoPopup(IEvent evt)
        {
            var data = (ShowInfoPopupEvent)evt;
            
            txt_attackPower.text = "AttackPower : " + data.Data.AttackPower;
            txt_name.text = "Name : " + data.Data.Attributes.Name;
            txt_exp.text = "XP : " + data.Data.LevelDataLogic.Xp;
            txt_level.text = "Level : " + data.Data.LevelDataLogic.Level;
            
            infoPopup.transform.position = data.ScreenPosition;
            Show();
        }
    }
}
