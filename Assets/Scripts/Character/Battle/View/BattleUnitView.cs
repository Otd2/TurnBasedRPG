using Character.Base;
using Health;
using Health.UI;
using UnityEngine;

namespace Character.Battle.View
{
    public class BattleUnitView : UnitView
    {
        #region Fields
        [SerializeField] private SpriteRenderer heroSprite;
        [SerializeField] private HPBarView healthBarView;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private CharacterAnimationController animationController;
        #endregion

        #region Properties
        public CharacterAnimationController AnimationController => animationController;
        public Vector3 GetAttackPosition() => attackPoint.position;
        #endregion

        private void Awake()
        {
            heroSprite.transform.localScale = Vector3.zero;
        }

        public void ConnectHpBar(HealthPoints health)
        {
            healthBarView.SetTotalHp(health.TotalHp);
            healthBarView.SetHp(health.GetHp());
            health.OnHealthChanged += healthBarView.SetHp;
        }
        
        public void SetSprite(Sprite sprite)
        {
            heroSprite.sprite = sprite;
        }
    }
}