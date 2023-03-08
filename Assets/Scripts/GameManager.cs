using Attack;
using BattleStates;
using Character;
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
        SetServices();
        ServiceLocator.Instance.DataProvideService.Load();
        
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

    private void SetServices()
    {
        ServiceLocator.Instance.DataProvideService = new LocalDataProvider();
        ServiceLocator.Instance.EndGameReward = new OnlyExpReward(1);
        ServiceLocator.Instance.HealthLogicService = new HealthLogicService(GameConfig.HealthIncreasePercentOnEachLevel);
        ServiceLocator.Instance.AttackPowerLogicService = new AttackPowerLogicService(GameConfig.AttackIncreasePercentOnEachLevel);
        ServiceLocator.Instance.LevelUpLogicService = new LevelUpLogicService(GameConfig.LevelUpOnEachXp);
        ServiceLocator.Instance.CharacterUnlockLogicService = new CharacterUnlockWithBattleCount
            (GameConfig.CharacterUnlockAfterBattle, _persistantDataManager);
    }

    private void OnLoadCompleted()
    {
        //Check if battle is on 
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