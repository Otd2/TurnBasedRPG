using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace Character
{
    public class CharacterSelectingUI : MonoBehaviour
    {
        [SerializeField] private Button BattleStartButton;
        [SerializeField] private GameObject DummyCharacterUIView;
        [SerializeField] private Transform charactersUIParent;
        
        private List<CharacterController> _createdCharacters;
        private List<GameObject> _dummyButtons;
        private PlayerPrefsPersistentDataManager _playerPrefsPersistentDataManager;
        private CharacterUIViewFactory _uiViewFactory;
        private GameInitializer _gameInitializer;

        public void Init(GameConfig config, PlayerPrefsPersistentDataManager playerPrefsPersistentDataManager,
            GameInitializer gameInitializer)
        {
            BattleStartButton.onClick.AddListener(OnBattleStarted);
            
            _gameInitializer = gameInitializer;
            _playerPrefsPersistentDataManager = playerPrefsPersistentDataManager;
            
            _uiViewFactory =
                new CharacterUIViewFactory(config.characterUIView, playerPrefsPersistentDataManager);
            
            CharacterUIController.OnAnyCharacerSelected += OnHeroSelectClicked;
            CharacterUIController.OnAnyCharacterUnselected += OnHeroSelectionRemoved;
        }


        public void SetUI()
        {
            //Clear
            if (_createdCharacters != null)
            {
                foreach (var characterController in _createdCharacters)
                {
                    characterController.Destroy();
                }

                foreach (var dummyButton in _dummyButtons)
                {
                    Destroy(dummyButton);
                }
            }

            _dummyButtons = new List<GameObject>();
            _createdCharacters = new List<CharacterController>();
            
            if (IsUnlockANewCharacter())
            {
                var alreadyUnlockedCharacterIds =
                    _playerPrefsPersistentDataManager.CurrentGameData.CharacterData.Keys.ToList();
                var nextUnlockedHero = ServiceLocator.Instance.DataProvideService.GetRandomHeroWithoutThisIds(alreadyUnlockedCharacterIds);
                _playerPrefsPersistentDataManager.OnCharacterUnlocked(nextUnlockedHero.ID);
            }

            //Create new Characters
            foreach (var characterData in _playerPrefsPersistentDataManager.CurrentGameData.CharacterData)
            {
                var heroAttributes =
                    ServiceLocator.Instance.DataProvideService.GetHeroAttributeWithId(characterData.Key);
                _createdCharacters.Add(_uiViewFactory.Create(characterData.Key, heroAttributes, charactersUIParent));       
            }
            
            //Create Empty Buttons
            for (int i = _playerPrefsPersistentDataManager.CurrentGameData.CharacterData.Count; i < 10; i++)
            {
                _dummyButtons.Add(Instantiate(DummyCharacterUIView, charactersUIParent));
            }

            UpdateSelectedHeroes();
        }

        private bool IsUnlockANewCharacter()
        {
            var currentUnlockedHeroCount = _playerPrefsPersistentDataManager.CurrentGameData.CharacterData.Count;
            if (currentUnlockedHeroCount >= 10)
                return false;

            var neededUnlockedHeroCount = 3 + Mathf.FloorToInt(_playerPrefsPersistentDataManager.CurrentGameData.totalMatchCount / 1f);
            return currentUnlockedHeroCount < neededUnlockedHeroCount;
        }

        private void OnDestroy()
        {
            CharacterUIController.OnAnyCharacerSelected -= OnHeroSelectClicked;
            CharacterUIController.OnAnyCharacterUnselected -= OnHeroSelectionRemoved;
        }

        private void OnHeroSelectionRemoved(int id)
        {
            if (_playerPrefsPersistentDataManager.CurrentGameData.selectedHeroes.Count == 3)
            {
                return;
            }
            UpdateSelectedHeroes();
        }
        
        private void OnHeroSelectClicked(int id)
        {
            UpdateSelectedHeroes();
        }

        private void UpdateSelectedHeroes()
        {
            UpdateBattleButtonActivity();
        }

        private void UpdateBattleButtonActivity()
        {
            //TODO : take this to its own class for Button and the magic
            //number needs to be come from the config class
            BattleStartButton.interactable =
                _playerPrefsPersistentDataManager.CurrentGameData.selectedHeroes.Count == 3;
        }

        private void OnBattleStarted()
        {
            //CREATE NEW BATTLE DATA
            transform.gameObject.SetActive(false);
            _gameInitializer.SetBattle();
        }

    }
}