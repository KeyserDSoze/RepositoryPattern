namespace RepositoryPattern.Population
{
    public interface INumberPopulationService<T, TKey> : IRandomPopulationService<T, TKey>
        where TKey : notnull
    { }
}