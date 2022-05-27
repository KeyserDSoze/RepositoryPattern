namespace RepositoryPattern.Population
{
    public interface IEnumerablePopulationService<T, TKey> : IRandomPopulationService<T, TKey>
        where TKey : notnull
    { }
}