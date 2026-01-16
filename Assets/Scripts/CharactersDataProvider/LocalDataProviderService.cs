using System.Collections.Generic;
using System.Linq;
using Character;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CharactersDataProvider
{
    public class LocalDataProviderService : IDataProviderService
    {
        private CharacterAttributesSo[] _heroAttributes;
        private CharacterAttributesSo[] _enemyAttributes;

        public void Initialize()
        {
            _heroAttributes = Resources.LoadAll<CharacterAttributesSo>("Heroes");
            _enemyAttributes = Resources.LoadAll<CharacterAttributesSo>("Enemies");
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

        public CharacterAttributesSo GetRandomHero()
        {
            var randomIndex = Random.Range(0, _heroAttributes.Length);
            return _heroAttributes[randomIndex];
        }

        public CharacterAttributesSo GetRandomHeroWithoutThisIds(List<int> ids)
        {
            var heroes = _heroAttributes
                .Where(so => !ids.Contains(so.ID)).ToList();

            var randomIndex = Random.Range(0, heroes.Count);
            return heroes[randomIndex];
        }

        public CharacterAttributesSo GetRandomEnemy()
        {
            var randomIndex = Random.Range(0, _enemyAttributes.Length);
            return _enemyAttributes[randomIndex];
        }
    }
}
