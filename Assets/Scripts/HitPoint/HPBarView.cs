using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace HitPoint
{
    public class HPBarView : MonoBehaviour
    {
        [SerializeField] private Image _hpBar;
        private int _totalHp;

        public void SetTotalHp(int totalHP)
        {
            _totalHp = totalHP;
        }
        
        public void SetHp(int newHP)
        {
            var newHpBarFillAmount = (float)newHP / _totalHp;
            _hpBar.DOFillAmount(newHpBarFillAmount, 0.2f);
        }
    }
}