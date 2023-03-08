using DG.Tweening;
using UnityEngine;

namespace Character.Base
{
    public abstract class UnitView : MonoBehaviour
    {
        protected CharacterController Controller;

        public virtual void SetController(CharacterController controller)
        {
            Controller = controller;
        }

        public void Destroy()
        {
            transform.DOKill();
            Destroy(gameObject);
        }
    }
}