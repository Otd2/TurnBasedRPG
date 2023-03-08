using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using System.Linq;
using Character.Menu;
using MiscUtil.Collections.Extensions;
using PersistentData;
using UnityEngine.Events;
using UnityEngine.UI;
using CharacterController = Character.Base.CharacterController;

namespace Character
{
    public class CharacterSelectingUI : MonoBehaviour
    {
        [SerializeField] private Button battleStartButton;
        [SerializeField] private GameObject dummyCharacterUIView;
        [SerializeField] private CharacterUIView characterUiView;
        [SerializeField] private Transform charactersUIParent;
        
        private List<CharacterController> _createdCharacters;
        private List<GameObject> _dummyButtons;
        private PersistantDataManager _persistentDataManager;
        private MenuCharacterFactory _factory;

        public void Init(PersistantDataManager persistentDataManager, UnityAction onBattleStarted)
        {
            battleStartButton.onClick.AddListener(onBattleStarted);
            _persistentDataManager = persistentDataManager;
            
            _factory =
                new MenuCharacterFactory(characterUiView, persistentDataManager);
            
            CharacterUIController.OnAnyCharacterSelected += OnHeroSelectClicked;
            CharacterUIController.OnAnyCharacterUnselected += OnHeroSelectionRemoved;
        }

        private void OnDestroy()
        {
            CharacterUIController.OnAnyCharacterSelected -= OnHeroSelectClicked;
            CharacterUIController.OnAnyCharacterUnselected -= OnHeroSelectionRemoved;
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
            //Create Empty Slots
            for (int i = _persistentDataManager.CurrentGameData.CharactersData.Count; i < GameConfig.MaxHeroCount; i++)
            {
                _dummyButtons.Add(Instantiate(dummyCharacterUIView, charactersUIParent));
            }
        }

        private void CreateCharacters()
        {
            //Create new Characters
            foreach (var characterData in _persistentDataManager.CurrentGameData.CharactersData)
            {
                var heroAttributes =
                    ServiceLocator.Instance.DataProvideService.GetHeroAttributeWithId(characterData.Key);
                _createdCharacters.Add(_factory.Create(characterData.Key, heroAttributes, charactersUIParent));
            }
        }

        private void UnlockNewHeroIfNeeded()
        {
            //Unlock new hero if needed
            if (ServiceLocator.Instance.CharacterUnlockLogicService.IsNewCharacterUnlock())
            {
                var alreadyUnlockedCharacterIds =
                    _persistentDataManager.GetCharactersData().Keys.ToList();

                var nextUnlockedHero =
                    ServiceLocator.Instance.DataProvideService.GetRandomHeroWithoutThisIds(alreadyUnlockedCharacterIds);

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
        
        private void OnHeroSelectionRemoved(int id)
        {
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
            battleStartButton.interactable =
                _persistentDataManager.CurrentGameData.selectedHeroes.Count == GameConfig.RequiredHeroCountForBattle;
        }

    }
}