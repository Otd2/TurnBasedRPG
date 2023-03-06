using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Character
{
    public abstract class AbstractCharacterFactory
    {
        protected readonly UnitView UnitPrefab;
        protected readonly PlayerPrefsPersistentDataManager _playerPrefsPersistentDataManager;

        protected AbstractCharacterFactory(UnitView unitPrefab, PlayerPrefsPersistentDataManager playerPrefsPersistentDataManager)
        {
            UnitPrefab = unitPrefab;
            _playerPrefsPersistentDataManager = playerPrefsPersistentDataManager;
        }

        public abstract CharacterController Create(int characterId, CharacterAttributes characterAttributes,
            Transform parent);
    }
}