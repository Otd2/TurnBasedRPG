using DG.Tweening;
using UnityEngine;

namespace Character
{
    public abstract class UnitView : MonoBehaviour
    {
        protected CharacterController _controller;

        public virtual void SetController(CharacterController controller)
        {
            this._controller = controller;
        }

        public virtual void Destroy()
        {
            transform.DOKill();
            Destroy(gameObject);
        }
    }
}