namespace RepositoryPattern.Population
{
    public interface ITimePopulationService<T, TKey> : IRandomPopulationService<T, TKey>
        where TKey : notnull
    { }
}