using DefaultNamespace;
using UnityEngine;

namespace Character
{
    public class PlayerBattleUnitFactory : AbstractCharacterFactory
    {
        private readonly ITurnManager _turnManager;

        public PlayerBattleUnitFactory(UnitView unitPrefab, PlayerPrefsPersistentDataManager playerPrefsPersistentDataManager, ITurnManager turnManager) : 
            base(unitPrefab, playerPrefsPersistentDataManager)
        {
            _turnManager = turnManager;
        }

        public override CharacterController Create(int characterId, CharacterAttributes characterAttributes, Transform parent)
        {
            CharacterData data = _playerPrefsPersistentDataManager.GetCharacterData(characterId);
            
            var battleModel = new UnitBattleModel(characterId, data.Lvl, data.Exp, characterAttributes, 
                    _playerPrefsPersistentDataManager.CurrentGameData.BattleData.CharactersWithHP[characterId]);
            
            var view = Object.Instantiate(UnitPrefab, parent);
            view.transform.localPosition = Vector3.zero;
            return new HeroBattleController((BattleUnitView)view, battleModel, _playerPrefsPersistentDataManager, _turnManager);
        }
    
    }
}