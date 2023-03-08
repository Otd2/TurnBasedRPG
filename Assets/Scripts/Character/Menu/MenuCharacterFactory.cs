using System.Linq;
using Character.Base;
using Character.Menu;
using DefaultNamespace;
using PersistentData;
using UnityEngine;
using CharacterController = Character.Base.CharacterController;

namespace Character
{
    public class MenuCharacterFactory : AbstractCharacterFactory
    {
        public MenuCharacterFactory(UnitView unitPrefab, PersistantDataManager persistentDataManager) : base(unitPrefab, persistentDataManager)
        {
        }

        public override CharacterController Create(int characterId, CharacterAttributes characterAttributes, Transform parent)
        {
            var data = PersistentDataManager.GetCharacterData(characterId);
            
            var uiModel = new UnitUIModel(characterId, data.Lvl, data.Exp, characterAttributes,
                PersistentDataManager);
            var view = Object.Instantiate(UnitPrefab, parent);
            return new CharacterUIController(view, uiModel);
        }
    }
}