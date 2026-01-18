namespace Config
{
    public class GameConfigService : IGameConfigService
    {
        public GameConfig Config { get; }
    
        public GameConfigService(GameConfigSO configSO)
        {
            Config = configSO.Config;
        }
    
        public void Initialize() { }
    }
}
