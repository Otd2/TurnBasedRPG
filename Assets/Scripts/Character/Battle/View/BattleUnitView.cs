using DefaultNamespace.Health;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Character
{
    public class BattleUnitView : UnitView
    {
        [SerializeField] private SpriteRenderer heroSprite;
        [SerializeField] private HPBarView healthBarView;
        [SerializeField] private Transform attackPoint;

        public Vector3 GetAttackPoint() => attackPoint.position;

        public void SetHpBar(DynamicHealth health)
        {
            healthBarView.SetTotalHp(health.TotalHp);
            healthBarView.SetHp(health.GetHp());
            health.OnHealthChanged += healthBarView.SetHp;
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        public void SetSprite(Sprite sprite)
        {
            heroSprite.sprite = sprite;
        }
    }
}