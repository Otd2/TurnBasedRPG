using System;
using System.Collections.Generic;
using System.Linq;
using Character;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourcesDataProvider : IDataProvideService
{
    public Action LoadCompletedEvent { get; }
    private CharacterAttributesSO[] _heroAttributes;
    private CharacterAttributesSO[] _enemyAttributes;

    public ResourcesDataProvider()
    {
    }

    public void Load()
    {
        _heroAttributes = Resources.LoadAll<CharacterAttributesSO>("Heroes");
        _enemyAttributes = Resources.LoadAll<CharacterAttributesSO>("Enemies");
        Debug.Log(_heroAttributes.Length);
        Debug.Log(_enemyAttributes.Length);
        LoadCompletedEvent?.Invoke();
    }

    public CharacterAttributes GetHeroAttributeWithId(int id)
    {
        var hero = _heroAttributes.First(so => so.ID == id);
        return hero.Attributes;
    }

    public CharacterAttributes GetEnemyAttributeWithId(int id)
    {
        var enemy = _enemyAttributes.First(so => so.ID == id);
        return enemy.Attributes;
    }

    public CharacterAttributesSO GetRandomHero()
    {
        var randomIndex = Random.Range(0, _heroAttributes.Length);
        return _heroAttributes[randomIndex];
    }

    public CharacterAttributesSO GetRandomHeroWithoutThisIds(List<int> ids)
    {
        var heroes = _heroAttributes
            .Where(so => !ids.Contains(so.ID)).ToList();

        var randomIndex = Random.Range(0, heroes.Count);
        return heroes[randomIndex];
    }

    public CharacterAttributesSO GetRandomEnemy()
    {
        var randomIndex = Random.Range(0, _enemyAttributes.Length);
        return _enemyAttributes[randomIndex];
    }
}