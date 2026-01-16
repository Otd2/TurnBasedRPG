public class ExpRewardService : IRewardService
{
    private readonly int _expAmount;

    public ExpRewardService(int expAmount)
    {
        _expAmount = expAmount;
    }

    public void Initialize() { }

    public int GetRewardedExp()
    {
        return _expAmount;
    }
}
