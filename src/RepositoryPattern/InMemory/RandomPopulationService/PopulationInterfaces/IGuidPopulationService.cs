namespace RepositoryPattern.Population
{
    public interface IGuidPopulationService<T, TKey> : IRandomPopulationService<T, TKey>
        where TKey : notnull
    { }
}