using Character;
using CharactersDataProvider;
using HitPoint;
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
            IDataProviderService dataProvider = ServiceLocator.Instance.Get<IDataProviderService>();
            BattleData battleData = new BattleData();
            battleData.CharactersWithHP = new BattleCharacterDictionary();
            
            foreach (var selectedHero in _dataManager.CurrentGameData.selectedHeroes)
            {
                CharacterData characterData = _dataManager.GetCharacterData(selectedHero);
                CharacterAttributes characterAttributes = dataProvider.GetHeroAttributeWithId(selectedHero);
                int totalHealth = ServiceLocator.Instance.Get<IHealthLogicService>().GetTotalHealth(characterAttributes.BaseHealth, characterData.Lvl);
                battleData.CharactersWithHP.Add(selectedHero, totalHealth);
            }

            CharacterAttributesSo randomEnemy = dataProvider.GetRandomEnemy();
            battleData.enemyId = randomEnemy.ID;
            battleData.enemyHp = randomEnemy.Attributes.BaseHealth;
            _dataManager.CurrentGameData.BattleData = battleData;
        }
    }
}