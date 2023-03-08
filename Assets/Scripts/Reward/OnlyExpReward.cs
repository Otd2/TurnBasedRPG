public class OnlyExpReward : IReward
{
    private readonly int _expAmount;
    
    public int GetRewardedExp()
    {
        return _expAmount;
    }
    public OnlyExpReward(int expAmount)
    {
        _expAmount = expAmount;
    }
}