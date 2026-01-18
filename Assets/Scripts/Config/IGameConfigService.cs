using Core;
namespace Config
{
    public interface IGameConfigService : IService
    {
        GameConfig Config { get; }
    }
}
