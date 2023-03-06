using System.Linq;
using DefaultNamespace;
using UnityEngine;

namespace Character
{
    public class CharacterUIViewFactory : AbstractCharacterFactory
    {
        private readonly CharacterSelectionEventManager _characterSelectionEventManager;

        public CharacterUIViewFactory(UnitView unitPrefab, PlayerPrefsPersistentDataManager playerPrefsPersistentDataManager) : base(unitPrefab, playerPrefsPersistentDataManager)
        {
        }

        public override CharacterController Create(int characterId, CharacterAttributes characterAttributes, Transform parent)
        {
            UnitUIModel uiModel;
            CharacterData data = _playerPrefsPersistentDataManager.GetCharacterData(characterId);
            if (data == null)
            {
                uiModel = new UnitUIModel(characterId, 1, 0, characterAttributes, 
                    false, false);
            }
            else
            {
                uiModel = new UnitUIModel(characterId, data.Lvl, data.Exp, characterAttributes, 
                    true, _playerPrefsPersistentDataManager.CurrentGameData.selectedHeroes.Contains(characterId));
            }
            
            var view = Object.Instantiate(UnitPrefab, parent);
            return new CharacterUIController(view, uiModel, _playerPrefsPersistentDataManager);
        }
        
        public UnitView CreateEmptyButton(Transform parent)
        {
            var view = Object.Instantiate(UnitPrefab, parent);
            return view;
        }
    }
}