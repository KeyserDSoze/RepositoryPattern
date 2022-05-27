using System.Security.Cryptography;

namespace RepositoryPattern.Population
{
    internal class CharPopulationService<T, TKey> : ICharPopulationService<T, TKey>
        where TKey : notnull
    {
        public dynamic GetValue(Type type, IPopulationService<T, TKey> populationService, int numberOfEntities, string treeName, dynamic args)
            => (char)RandomNumberGenerator.GetInt32(256);
    }
}