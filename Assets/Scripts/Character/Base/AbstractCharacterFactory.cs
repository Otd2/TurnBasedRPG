using PersistentData;
using UnityEngine;

namespace Character.Base
{
    public abstract class AbstractCharacterFactory
    {
        protected readonly UnitView UnitPrefab;
        protected readonly PersistantDataManager PersistentDataManager;

        protected AbstractCharacterFactory(UnitView unitPrefab, PersistantDataManager persistentDataManager)
        {
            UnitPrefab = unitPrefab;
            PersistentDataManager = persistentDataManager;
        }

        public abstract CharacterController Create(int characterId, CharacterAttributes characterAttributes,
            Transform parent);
    }
}