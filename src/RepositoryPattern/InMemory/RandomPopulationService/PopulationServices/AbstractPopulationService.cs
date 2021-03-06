namespace RepositoryPattern.Population
{
    internal class AbstractPopulationService<T, TKey> : IAbstractPopulationService<T, TKey>
        where TKey : notnull
    {
        public dynamic GetValue(Type type, IPopulationService<T,TKey> populationService, int numberOfEntities, string treeName, dynamic args)
        {
            return null!;
        }
    }
}