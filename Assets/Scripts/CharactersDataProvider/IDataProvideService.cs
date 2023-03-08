using System;
using System.Collections.Generic;
using Character;

namespace CharactersDataProvider
{
    //This can be extendable to Addressable, cloud etc.
    public interface IDataProvideService
    {
        public Action LoadCompletedEvent { get; }
        public void Load();
        public CharacterAttributes GetHeroAttributeWithId(int id);
        public CharacterAttributes GetEnemyAttributeWithId(int id);
        public CharacterAttributesSo GetRandomHeroWithoutThisIds(List<int> ids);
        public CharacterAttributesSo GetRandomHero();
        public CharacterAttributesSo GetRandomEnemy();
    }
}