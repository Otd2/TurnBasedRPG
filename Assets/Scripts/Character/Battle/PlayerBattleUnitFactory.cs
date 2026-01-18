using Battle.StateMachine;
using Character.Base;
using Character.Battle.Controller;
using Character.Battle.Model;
using Character.Battle.View;
using Persistence;
using UnityEngine;
using CharacterController = Character.Base.CharacterController;

namespace Character.Battle
{
    public class PlayerBattleUnitFactory : AbstractCharacterFactory
    {
        private readonly IBattleStateMachine _battleStateMachine;

        public PlayerBattleUnitFactory(UnitView unitPrefab, PersistentDataManager persistentDataManager, IBattleStateMachine battleStateMachine) : 
            base(unitPrefab, persistentDataManager)
        {
            _battleStateMachine = battleStateMachine;
        }

        public override CharacterController Create(int characterId, CharacterAttributes characterAttributes, Transform parent)
        {
            //Get saved data
            CharacterData data = PersistentDataManager.GetCharacterData(characterId);
            //Create model
            var battleModel = new HeroBattleModel(characterId, data.Lvl, data.Exp, characterAttributes, PersistentDataManager);
            //Create view
            var view = Object.Instantiate(UnitPrefab, parent);
            view.transform.localPosition = Vector3.zero;
            //return controller
            return new HeroBattleController((BattleUnitView)view, battleModel, _battleStateMachine);
        }
    
    }
}