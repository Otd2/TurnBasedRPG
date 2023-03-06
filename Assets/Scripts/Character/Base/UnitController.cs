using DefaultNamespace;

namespace Character
{
    public abstract class CharacterController
    {
        protected PlayerPrefsPersistentDataManager PlayerPrefsPersistentDataManager;

        protected CharacterController(UnitView view, UnitModelBase model, PlayerPrefsPersistentDataManager playerPrefsPersistentDataManager)
        {
            PlayerPrefsPersistentDataManager = playerPrefsPersistentDataManager;
        }

        protected abstract void AssignView(UnitView view);

        public abstract void Destroy();
    }
}