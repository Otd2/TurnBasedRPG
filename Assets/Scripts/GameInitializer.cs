using System.Collections;
using System.Collections.Generic;
using Character;
using DefaultNamespace;
using DefaultNamespace.Attack;
using InfoPopupController;
using Level;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private PlayerPrefsPersistentDataManager _playerPrefsPersistentDataManager;
    [SerializeField] private GameConfig _config;
    [SerializeField] private CharacterSelectingUI _characterSelectingUI;
    [SerializeField] private BattleBoardController _battleBoardController;
    private BattleDataCreator _battleDataCreator;

    void Start()
    {
        SetServices();
        ServiceLocator.Instance.DataProvideService.Load();
        
        _playerPrefsPersistentDataManager = new PlayerPrefsPersistentDataManager(_config);
        _battleDataCreator = new BattleDataCreator(_playerPrefsPersistentDataManager);
        
        _characterSelectingUI.Init(_config,
            _playerPrefsPersistentDataManager, 
            this);
        
        _battleBoardController.Init(_config, _playerPrefsPersistentDataManager, this);
        
        _playerPrefsPersistentDataManager.LoadData(OnLoadCompleted);
    }

    private void SetServices()
    {
        ServiceLocator.Instance.DataProvideService = new ResourcesDataProvider();
        ServiceLocator.Instance.EndGameReward = new OnlyExpReward(1);
        ServiceLocator.Instance.HealthLogicService = new HealthLogicService(_config.healthIncreasePercentOnEachLevel);
        ServiceLocator.Instance.AttackIncreaseLogicService = new AttackIncreaseLogicService(_config.attackIncreasePercentOnEachLevel);
        ServiceLocator.Instance.LevelUpLogicService = new LevelUpLogicService(_config.levelUpOnEachXp);
    }

    private void OnLoadCompleted()
    {
        //Check if battle is on 
        if(_playerPrefsPersistentDataManager.CurrentGameData.BattleData == null || _playerPrefsPersistentDataManager.CurrentGameData.BattleData.CharactersWithHP.Count == 0)
            SetUI();
        else
            SetBattle();
    }

    public void SetUI()
    {
        _characterSelectingUI.gameObject.SetActive(true);
        _characterSelectingUI.SetUI();
    }

    public void SetBattle()
    {
        //TODO createBattleData
        if(_playerPrefsPersistentDataManager.CurrentGameData.BattleData == null || _playerPrefsPersistentDataManager.CurrentGameData.BattleData.CharactersWithHP.Count == 0)
            _battleDataCreator.CreateNewBattleData();
        
        _characterSelectingUI.transform.root.gameObject.SetActive(false);
        _battleBoardController.CreateBattleBoard();
    }
}

interface IGameState
    {
        public void Enter();

        public void Update();

        public void Exit();
    }

    public class Load : IGameState
    {
        private readonly PlayerPrefsPersistentDataManager _playerPrefsPersistentDataManager;
        private readonly GameInitializer _gameInitializer;

        public Load(PlayerPrefsPersistentDataManager playerPrefsPersistentDataManager, GameInitializer gameInitializer)
        {
            _playerPrefsPersistentDataManager = playerPrefsPersistentDataManager;
            _gameInitializer = gameInitializer;
        }
        
        public void Enter()
        {
            _playerPrefsPersistentDataManager.LoadData(Exit);
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            //gameInitializer.
        }
    }
    
    public class Menu : IGameState
    {
        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
    
    public class Battle : IGameState
    {
        //
        //public 
        
        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
