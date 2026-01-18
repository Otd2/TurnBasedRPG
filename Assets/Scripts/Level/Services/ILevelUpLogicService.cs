using Core;

namespace Level.Services
{
    public interface ILevelUpLogicService : IService
    {
        bool IsLevelUp(int xp, int level);
    }
}
