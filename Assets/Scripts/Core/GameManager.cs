using Battle;
using Character;
using Character.Interfaces;
using Character.Services;
using CharactersDataProvider;
using Combat.Services;
using Config;
using Health.Services;
using Level.Services;
using Persistence;
using Reward;
using UI;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameConfigSO config;
        [SerializeField] private CharacterSelectingUI characterSelectingUI;
        [SerializeField] private BattleBoardController battleBoardController;
        
        private PersistentDataManager _persistentDataManager;
        private BattleDataCreator _battleDataCreator;

        void Start()
        {
            _persistentDataManager = new PlayerPrefsPersistentDataManager();
            RegisterServices();
            
            _battleDataCreator = new BattleDataCreator(_persistentDataManager);
            
            characterSelectingUI.Init(_persistentDataManager, SetBattle);
            battleBoardController.Init(_persistentDataManager, SetUI);
            
            _persistentDataManager.Load(OnLoadCompleted);
        }

#if UNITY_EDITOR
        void OnApplicationQuit()
        {
            _persistentDataManager.Save();
        }
#elif UNITY_ANDROID
        void OnApplicationPause(bool isPause)
        {
            if(isPause)
                _persistentDataManager.Save();
        }
#endif

        private void RegisterServices()
        {
            GameConfig gameConfig = config.Config;
            ServiceLocator.Instance.Register<IGameConfigService>(new GameConfigService(config));
            ServiceLocator.Instance.Register<IDataProviderService>(new LocalDataProviderService());
            ServiceLocator.Instance.Register<IRewardService>(new ExpRewardService(gameConfig.ExpPerWin));
            ServiceLocator.Instance.Register<IHealthLogicService>(new HealthLogicService(gameConfig.HealthIncreasePercentOnEachLevel));
            ServiceLocator.Instance.Register<IAttackPowerLogicService>(new AttackPowerLogicService(gameConfig.AttackIncreasePercentOnEachLevel));
            ServiceLocator.Instance.Register<ILevelUpLogicService>(new LevelUpLogicService(gameConfig.LevelUpOnEachXp));
            ServiceLocator.Instance.Register<ICharacterUnlockLogicService>(new CharacterUnlockService(gameConfig.CharacterUnlockAfterBattle, _persistentDataManager));
            
            ServiceLocator.Instance.Lock();
        }

        private void OnLoadCompleted()
        {
            if(_persistentDataManager.IsBattleOn())
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
            if(!_persistentDataManager.IsBattleOn())
                _battleDataCreator.CreateNewBattleData();
            
            battleBoardController.CreateBattleBoard();
        }
    }
}
