namespace RepositoryPattern.Population
{
    public interface IRegexPopulationService<T, TKey> : IRandomPopulationService<T, TKey>
        where TKey : notnull
    { }
}