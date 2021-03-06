namespace RepositoryPattern.Population
{
    public interface ICharPopulationService<T, TKey> : IRandomPopulationService<T, TKey>
        where TKey : notnull
    { }
}