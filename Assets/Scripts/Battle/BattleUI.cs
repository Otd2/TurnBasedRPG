using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Battle
{
    public class BattleUI : MonoBehaviour
    {
        [SerializeField] private GameObject playerTurnUI;
        [SerializeField] private GameObject endGameUI;
        [SerializeField] private TextMeshProUGUI endGameStatusText;
        [SerializeField] private Button menuButton;

        public void SetMenuButtonAction(UnityAction menuButtonAction)
        {
            menuButton.onClick.AddListener(menuButtonAction);
        }

        public void BattleStart()
        {
            endGameUI.gameObject.SetActive(false);
            playerTurnUI.gameObject.SetActive(false);
        }

        public void ShowPlayerTurnUI()
        {
            playerTurnUI.gameObject.SetActive(true);
        }

        public void HidePlayerTurnUI()
        {
            playerTurnUI.gameObject.SetActive(false);
        }

        public void ShowWinUI()
        {
            endGameStatusText.text = "WIN";
            endGameUI.gameObject.SetActive(true);
        }
        
        public void ShowLoseUI()
        {
            endGameStatusText.text = "LOSE";
            endGameUI.gameObject.SetActive(true);
        }
    }
}