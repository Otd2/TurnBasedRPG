using System;
using Character;
using TMPro;
using UnityEngine;

namespace InfoPopupController
{
    public class UnitInfoPopupController : PopupController, IInfoPopupController
    {
        [SerializeField] private TextMeshProUGUI txt_attackPower;
        [SerializeField] private TextMeshProUGUI txt_name;
        [SerializeField] private TextMeshProUGUI txt_exp;
        [SerializeField] private TextMeshProUGUI txt_level;
        
        [SerializeField] private RectTransform infoPopup;

        public void SetData(UnitModelBase infoPopupData, Vector2 screenPosition)
        {
            txt_attackPower.text = "AttackPower : " + infoPopupData.AttackPower;
            txt_name.text = "Name : " + infoPopupData.Attributes.Name;
            txt_exp.text = "XP : " + infoPopupData.LevelDataLogic.Xp;
            txt_level.text = "Level : " + infoPopupData.LevelDataLogic.Level;
            
            infoPopup.transform.position = screenPosition;
            Show();
        }
    }
}