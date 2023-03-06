using Character;
using DefaultNamespace;

public class BattleDataCreator
{
    private readonly PlayerPrefsPersistentDataManager _dataManager;

    public BattleDataCreator(PlayerPrefsPersistentDataManager dataManager)
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
        _dataManager.SaveData();
    }
}