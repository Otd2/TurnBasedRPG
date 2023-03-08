using Attack;
using CharactersDataProvider;
using DefaultNamespace;
using HitPoint;
using InfoPopupController;
using Level;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    [SerializeField] private UnitInfoPopupController _infoPopupController;
    public static ServiceLocator Instance { get; private set; }
    public IInfoPopupController InfoPopupController => _infoPopupController;
    
    public IReward EndGameReward;
    public IHealthLogicService HealthLogicService;
    public IAttackPowerLogicService AttackPowerLogicService;
    public IDataProvideService DataProvideService;
    public ILevelUpLogicService LevelUpLogicService;
    public ICharacterUnlockLogicService CharacterUnlockLogicService;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
}
