namespace RepositoryPattern.Population
{
    public interface IArrayPopulationService<T, TKey> : IRandomPopulationService<T, TKey>
        where TKey : notnull
    { }
}