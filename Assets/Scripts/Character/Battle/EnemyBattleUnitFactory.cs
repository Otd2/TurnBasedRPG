using DefaultNamespace;
using UnityEngine;

namespace Character
{
    public class EnemyBattleUnitFactory : PlayerBattleUnitFactory
    {
        public EnemyBattleUnitFactory(UnitView unitPrefab, PlayerPrefsPersistentDataManager playerPrefsPersistentDataManager, ITurnManager turnManager) 
            : base(unitPrefab, playerPrefsPersistentDataManager, turnManager)
        {
        }

        public override CharacterController Create(int characterId, CharacterAttributes characterAttributes, Transform parent)
        {
            return base.Create(characterId, characterAttributes, parent);
        }
    }
}