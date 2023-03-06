using System;
using System.Collections.Generic;
using Character;

public class AddressableDataProvideService : IDataProvideService
{
    public Action LoadCompletedEvent { get; }
    public void Load()
    {
        throw new NotImplementedException();
    }

    public CharacterAttributes GetHeroAttributeWithId(int id)
    {
        throw new NotImplementedException();
    }

    public CharacterAttributes GetEnemyAttributeWithId(int id)
    {
        throw new NotImplementedException();
    }

    public CharacterAttributesSO GetRandomHeroWithoutThisIds(List<int> ids)
    {
        throw new NotImplementedException();
    }

    public CharacterAttributesSO GetRandomHero()
    {
        throw new NotImplementedException();
    }

    public CharacterAttributesSO GetRandomEnemy()
    {
        throw new NotImplementedException();
    }
}