using Attack;
using BattleStates;
using Character;
using Character.Interfaces;
using Character.Services;
using CharactersDataProvider;
using HitPoint;
using Level;
using PersistentData;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CharacterSelectingUI characterSelectingUI;
    [SerializeField] private BattleBoardController battleBoardController;
    
    private PersistantDataManager _persistantDataManager;
    private BattleDataCreator _battleDataCreator;

    void Start()
    {
        _persistantDataManager = new PlayerPrefsPersistentDataManager();
        RegisterServices();
        
        _battleDataCreator = new BattleDataCreator(_persistantDataManager);
        
        characterSelectingUI.Init(_persistantDataManager, SetBattle);
        battleBoardController.Init(_persistantDataManager, SetUI);
        
        _persistantDataManager.Load(OnLoadCompleted);
    }

#if UNITY_EDITOR
    void OnApplicationQuit()
    {
        _persistantDataManager.Save();
    }
#elif UNITY_ANDROID
    void OnApplicationPause(bool isPause)
    {
        if(isPause)
            _persistantDataManager.Save();
    }
#endif

    private void RegisterServices()
    {
        ServiceLocator.Instance.Register<IDataProviderService>(new LocalDataProviderService());
        ServiceLocator.Instance.Register<IRewardService>(new ExpRewardService(1));
        ServiceLocator.Instance.Register<IHealthLogicService>(new HealthLogicService(GameConfig.HealthIncreasePercentOnEachLevel));
        ServiceLocator.Instance.Register<IAttackPowerLogicService>(new AttackPowerLogicService(GameConfig.AttackIncreasePercentOnEachLevel));
        ServiceLocator.Instance.Register<ILevelUpLogicService>(new LevelUpLogicService(GameConfig.LevelUpOnEachXp));
        ServiceLocator.Instance.Register<ICharacterUnlockLogicService>(new CharacterUnlockService(GameConfig.CharacterUnlockAfterBattle, _persistantDataManager));
        
        ServiceLocator.Instance.Lock();
    }

    private void OnLoadCompleted()
    {
        if(_persistantDataManager.IsBattleOn())
            SetBattle();
        else
            SetUI();
    }

    private void SetUI()
    {
        characterSelectingUI.gameObject.SetActive(true);
        characterSelectingUI.SetUI();
    }

    private void SetBattle()
    {
        characterSelectingUI.gameObject.SetActive(false);
        if(!_persistantDataManager.IsBattleOn())
            _battleDataCreator.CreateNewBattleData();
        
        battleBoardController.CreateBattleBoard();
    }
}
