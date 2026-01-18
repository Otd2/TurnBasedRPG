using Persistence;
using UnityEngine;

namespace Character.Base
{
    public abstract class AbstractCharacterFactory
    {
        protected readonly UnitView UnitPrefab;
        protected readonly PersistentDataManager PersistentDataManager;

        protected AbstractCharacterFactory(UnitView unitPrefab, PersistentDataManager persistentDataManager)
        {
            UnitPrefab = unitPrefab;
            PersistentDataManager = persistentDataManager;
        }

        public abstract CharacterController Create(int characterId, CharacterAttributes characterAttributes, Transform parent);
    }
}