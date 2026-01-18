using System.Collections.Generic;
using System.Linq;
using BattleStates.StateMachine;
using Character;
using Character.Battle;
using Character.Battle.Controller;
using Character.Battle.Model;
using Character.Battle.View;
using CharactersDataProvider;
using DefaultNamespace.Target;
using FloatingText;
using PersistentData;
using UnityEngine;
using UnityEngine.Events;

namespace BattleStates
{
    public class BattleBoardController : MonoBehaviour
    {
        [SerializeField] private Transform[] boardPositions;
        [SerializeField] private Transform enemyPosition;
        [SerializeField] private HeroView heroViewPrefab;
        [SerializeField] private BattleUnitView enemyViewPrefab;
        [SerializeField] private FloatingTextPoolController floatingTextPoolController;
        [SerializeField] private BattleUI _battleUI;
    
        private PlayerBattleUnitFactory _battleUnitFactory;
        private PersistantDataManager _persistentDataManager;
        private List<UnitBattleController> _playerControllers;
        private List<UnitBattleController> _enemyControllers;
    
        private IBattleStateMachine _battleStateMachine;
        private UnityAction _onBattleEnded;

        private void Start()
        {
            floatingTextPoolController.Init();
        }

        public void Init(PersistantDataManager persistentDataManager, UnityAction onBattleEnded)
        {
            _persistentDataManager = persistentDataManager;
            _battleStateMachine = new BattleStateMachine(this, _battleUI);
            _battleUnitFactory = new PlayerBattleUnitFactory(heroViewPrefab, _persistentDataManager, _battleStateMachine);
            _onBattleEnded = onBattleEnded;
            _battleUI.SetMenuButtonAction(ReturnToMenu);
        }

        public void CreateBattleBoard()
        {
            CreateHeroes();
            CreateEnemy();
            SetUnitsTargets();
            _battleUI.BattleStart();
            _battleStateMachine.InitBattle(_playerControllers, _enemyControllers);
        }

        void CreateHeroes()
        {
            _playerControllers = new List<UnitBattleController>();
            var dataProvider = ServiceLocator.Instance.Get<IDataProviderService>();
            for (var index = 0; index < boardPositions.Length; index++)
            {
                var boardPosition = boardPositions[index];
                var id = _persistentDataManager.CurrentGameData.selectedHeroes[index];
                var characterAttributes = dataProvider.GetHeroAttributeWithId(id);
                var playerController =
                    (HeroBattleController)_battleUnitFactory.Create(id, characterAttributes, boardPosition);
                _playerControllers.Add(playerController);
            }
        }
    
        void CreateEnemy()
        {
            _enemyControllers = new List<UnitBattleController>();
            
            int enemyId = _persistentDataManager.CurrentGameData.BattleData.enemyId;
        
            //model
            EnemyBattleModel enemyModel = new EnemyBattleModel(enemyId, 1, 0, ServiceLocator.Instance.Get<IDataProviderService>().GetEnemyAttributeWithId(enemyId), 
                _persistentDataManager);
        
            //view
            BattleUnitView enemyView = Instantiate(enemyViewPrefab, enemyPosition, false);
            enemyView.transform.localPosition = Vector3.zero;
        
            //controller
            _enemyControllers.Add(new EnemyBattleController(enemyView, enemyModel, _battleStateMachine));
        }

        void SetUnitsTargets()
        {
            foreach (var playerController in _playerControllers)
            {
                playerController.SetTargets(_enemyControllers.Cast<ITarget>().ToList());
            }
            foreach (var enemyController in _enemyControllers)
            {
                enemyController.SetTargets(_playerControllers.Cast<ITarget>().ToList());
            }
        }
        
        public void ClearBattleData()
        {
            _persistentDataManager.CurrentGameData.BattleData = null;
            _persistentDataManager.CurrentGameData.totalMatchCount++;
        }

        void ReturnToMenu()
        {
            foreach (var playerController in _playerControllers)
            {
                playerController.Destroy();
            }

            foreach (var enemyController in _enemyControllers)
            {
                enemyController.Destroy();
            }
        
            _onBattleEnded?.Invoke();
        }
    }
}


