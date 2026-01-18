using Events;

namespace Character.Base
{
    public abstract class CharacterController : EventLayer
    {

        protected CharacterController(UnitView view, UnitModelBase model)
        {
        }

        protected abstract void AssignView(UnitView view);

        public abstract void Destroy();
    }
}