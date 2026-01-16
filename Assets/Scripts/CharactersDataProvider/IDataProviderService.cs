using System.Collections.Generic;
using Character;

namespace CharactersDataProvider
{
    public interface IDataProviderService : IService
    {
        CharacterAttributes GetHeroAttributeWithId(int id);
        CharacterAttributes GetEnemyAttributeWithId(int id);
        CharacterAttributesSo GetRandomHeroWithoutThisIds(List<int> ids);
        CharacterAttributesSo GetRandomHero();
        CharacterAttributesSo GetRandomEnemy();
    }
}
