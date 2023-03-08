using DefaultNamespace;
using PersistentData;

namespace BattleStates
{
    public class BattleDataCreator
    {
        private readonly PersistantDataManager _dataManager;

        public BattleDataCreator(PersistantDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public void CreateNewBattleData()
        {
            var dataProvider = ServiceLocator.Instance.DataProvideService;
            BattleData battleData = new BattleData();
            battleData.CharactersWithHP = new BattleCharacterDictionary();
            
            foreach (var selectedHero in _dataManager.CurrentGameData.selectedHeroes)
            {
                var characterData = _dataManager.GetCharacterData(selectedHero);
                var characterAttributes = dataProvider.GetHeroAttributeWithId(selectedHero);
                var totalHealth = ServiceLocator.Instance.HealthLogicService.GetTotalHealth(characterAttributes.BaseHealth, characterData.Lvl);
                battleData.CharactersWithHP.Add(selectedHero, totalHealth);
            }

            var randomEnemy = dataProvider.GetRandomEnemy();
            battleData.enemyId = randomEnemy.ID;
            battleData.enemyHp = randomEnemy.Attributes.BaseHealth;
            _dataManager.CurrentGameData.BattleData = battleData;
        }
    }
}