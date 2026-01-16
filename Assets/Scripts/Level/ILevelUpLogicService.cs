namespace Level
{
    public interface ILevelUpLogicService : IService
    {
        bool IsLevelUp(int xp, int level);
    }
}
