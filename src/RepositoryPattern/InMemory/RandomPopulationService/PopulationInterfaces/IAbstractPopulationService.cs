namespace RepositoryPattern.Population
{
    public interface IAbstractPopulationService<T, TKey> : IRandomPopulationService<T, TKey>
        where TKey : notnull
    { }
}