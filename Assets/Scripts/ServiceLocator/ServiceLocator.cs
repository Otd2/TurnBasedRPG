using DefaultNamespace;
using DefaultNamespace.Attack;
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
    public IAttackIncreaseLogicService AttackIncreaseLogicService;
    public IDataProvideService DataProvideService;
    public ILevelUpLogicService LevelUpLogicService;

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