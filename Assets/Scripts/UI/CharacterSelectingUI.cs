using System.Collections.Generic;
using System.Linq;
using Character.Interfaces;
using Character.Menu;
using CharactersDataProvider;
using Config;
using Core;
using Events;
using Events.Interfaces;
using Persistence;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using CharacterController = Character.Base.CharacterController;

namespace UI
{
    public class CharacterSelectingUI : MonoBehaviour
    {
        [SerializeField] private Button battleStartButton;
        [SerializeField] private GameObject dummyCharacterUIView;
        [SerializeField] private CharacterUIView characterUiView;
        [SerializeField] private Transform charactersUIParent;
        
        private List<CharacterController> _createdCharacters;
        private List<GameObject> _dummyButtons;
        private PersistentDataManager _persistentDataManager;
        private MenuCharacterFactory _factory;

        public void Init(PersistentDataManager persistentDataManager, UnityAction onBattleStarted)
        {
            battleStartButton.onClick.AddListener(onBattleStarted);
            _persistentDataManager = persistentDataManager;
            
            _factory = new MenuCharacterFactory(characterUiView, persistentDataManager);
            
            EventBus.Subscribe(EventNames.CharacterSelected, OnHeroSelectClicked);
            EventBus.Subscribe(EventNames.CharacterUnselected, OnHeroSelectionRemoved);
        }

        private void OnDestroy()
        {
            EventBus.Unsubscribe(EventNames.CharacterSelected, OnHeroSelectClicked);
            EventBus.Unsubscribe(EventNames.CharacterUnselected, OnHeroSelectionRemoved);
        }

        public void SetUI()
        {
            ClearUI();
            
            UnlockNewHeroIfNeeded();
            CreateCharacters();
            CreateDummyButtons();
            UpdateSelectedHeroes();
        }

        private void CreateDummyButtons()
        {
            var config = ServiceLocator.Instance.Get<IGameConfigService>().Config;
            for (int i = _persistentDataManager.CurrentGameData.CharactersData.Count; i < config.MaxHeroCount; i++)
            {
                _dummyButtons.Add(Instantiate(dummyCharacterUIView, charactersUIParent));
            }
        }

        private void CreateCharacters()
        {
            //Create new Characters
            var dataProvider = ServiceLocator.Instance.Get<IDataProviderService>();
            foreach (var characterData in _persistentDataManager.CurrentGameData.CharactersData)
            {
                var heroAttributes = dataProvider.GetHeroAttributeWithId(characterData.Key);
                _createdCharacters.Add(_factory.Create(characterData.Key, heroAttributes, charactersUIParent));
            }
        }

        private void UnlockNewHeroIfNeeded()
        {
            //Unlock new hero if needed
            if (ServiceLocator.Instance.Get<ICharacterUnlockLogicService>().IsNewCharacterUnlock())
            {
                var alreadyUnlockedCharacterIds =
                    _persistentDataManager.GetCharactersData().Keys.ToList();

                var nextUnlockedHero =
                    ServiceLocator.Instance.Get<IDataProviderService>().GetRandomHeroWithoutThisIds(alreadyUnlockedCharacterIds);

                _persistentDataManager.OnCharacterUnlocked(nextUnlockedHero.ID);
            }
        }

        private void ClearUI()
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
        }
        
        private void OnHeroSelectionRemoved(IEvent evt)
        {
            UpdateSelectedHeroes();
        }
        
        private void OnHeroSelectClicked(IEvent evt)
        {
            UpdateSelectedHeroes();
        }

        private void UpdateSelectedHeroes()
        {
            UpdateBattleButtonActivity();
        }

        private void UpdateBattleButtonActivity()
        {
            var config = ServiceLocator.Instance.Get<IGameConfigService>().Config;
            battleStartButton.interactable =
                _persistentDataManager.CurrentGameData.selectedHeroes.Count == config.RequiredHeroCountForBattle;
        }

    }
}