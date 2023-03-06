using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using DefaultNamespace.Attack;
using DefaultNamespace.Target;
using UnityEngine;
using CharacterController = Character.CharacterController;

public class BattleBoardController : MonoBehaviour
{
    [SerializeField] private Transform[] boardPositions;
    [SerializeField] private Transform enemyPosition;
    
    private PlayerBattleUnitFactory _battleUnitFactory;
    private GameConfig _config;
    private PlayerPrefsPersistentDataManager _persistentDataManager;
    private List<CharacterBattleController> playerControllers;
    private List<CharacterBattleController> enemyControllers;
    private GameInitializer _gameInitializer;
    private ITurnManager _turnManager;
    
    [SerializeField] private FloatingTextPoolController _floatingTextPoolController;
    

    public void Init(GameConfig config, PlayerPrefsPersistentDataManager persistentDataManager, 
        GameInitializer gameInitializer)
    {
        _config = config;
        _persistentDataManager = persistentDataManager;
        _gameInitializer = gameInitializer;
        
        _turnManager = new TurnStateManager(OnStateChanged);
        
        _battleUnitFactory = new PlayerBattleUnitFactory(_config.heroView, _persistentDataManager, _turnManager);
        _floatingTextPoolController.Init();
    }

    public void CreateBattleBoard()
    {
        playerControllers = new List<CharacterBattleController>();
        enemyControllers = new List<CharacterBattleController>();
        for (var index = 0; index < boardPositions.Length; index++)
        {
            var boardPosition = boardPositions[index];
            var id = _persistentDataManager.CurrentGameData.selectedHeroes[index];
            var characterAttributes = ServiceLocator.Instance.DataProvideService.GetHeroAttributeWithId(id);
            var playerController =
                (HeroBattleController)_battleUnitFactory.Create(id, characterAttributes, boardPosition);
            playerControllers.Add(playerController);
            
        }

        CreateEnemy();
        SetAttackTargets();

        _turnManager.InitTurn(GameState.PlayerTurn, playerControllers, enemyControllers);
    }
    
    void CreateEnemy()
    {
        var enemyId = _persistentDataManager.CurrentGameData.BattleData.enemyId;
        var enemyHp = _persistentDataManager.CurrentGameData.BattleData.enemyHp;
        
        //model
        var enemyModel = new UnitBattleModel(enemyId, 1, 0, ServiceLocator.Instance.DataProvideService.GetEnemyAttributeWithId(enemyId), 
            enemyHp);
        
        //view
        var enemyView = Instantiate(_config.battleView, enemyPosition, false);
        enemyView.transform.localPosition = Vector3.zero;
        
        //controller
        enemyControllers.Add(new EnemyHeroBattleController(enemyView, enemyModel, _persistentDataManager, _turnManager));
    }

    private void SetAttackTargets()
    {
        foreach (var playerController in playerControllers)
        {
            playerController.SetAttackStrategy(enemyControllers.Cast<ITarget>().ToList());
        }
        foreach (var enemyController in enemyControllers)
        {
            enemyController.SetAttackStrategy(playerControllers.Cast<ITarget>().ToList());
        }
    }

    private void OnStateChanged(GameState obj)
    {
        if (obj == GameState.Win)
        {
            GiveReward();
            DelayedGameOverCall();
        }
        else if (obj == GameState.Lose)
        {
            DelayedGameOverCall();
        }
    }

    async void DelayedGameOverCall()
    {
        _persistentDataManager.CurrentGameData.BattleData = null;
        _persistentDataManager.CurrentGameData.totalMatchCount++;
        _persistentDataManager.SaveData();
        await UniTask.Delay(TimeSpan.FromSeconds(2f), ignoreTimeScale: false);
        ClearBattleArena();
        _gameInitializer.SetUI();
    }

    void ClearBattleArena()
    {
        foreach (var playerController in playerControllers)
        {
            playerController.Destroy();
        }

        foreach (var enemyController in enemyControllers)
        {
            enemyController.Destroy();
        }
    }

    
    private void GiveReward()
    {
        foreach (var playerController in playerControllers)
        {
            ((HeroBattleController)playerController).MatchEnded();
        }
    }


    public enum GameState
    {
        PlayerActionInProgress,
        EnemyActionInProgress,
        PlayerTurn,
        EnemyState,
        Win,
        Lose
    }
}


