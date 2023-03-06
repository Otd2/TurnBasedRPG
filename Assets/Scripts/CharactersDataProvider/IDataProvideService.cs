using System;
using System.Collections.Generic;
using Character;
using UnityEditorInternal.Profiling.Memory.Experimental;

public interface IDataProvideService
{
    public Action LoadCompletedEvent { get; }
    public void Load();
    public CharacterAttributes GetHeroAttributeWithId(int id);
    public CharacterAttributes GetEnemyAttributeWithId(int id);
    public CharacterAttributesSO GetRandomHeroWithoutThisIds(List<int> ids);
    public CharacterAttributesSO GetRandomHero();
    public CharacterAttributesSO GetRandomEnemy();
}