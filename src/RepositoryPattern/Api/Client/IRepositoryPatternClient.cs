using RepositoryPattern;

namespace RepositoryPattern.Client
{
    public interface IRepositoryPatternClient<T, TKey> : IRepositoryPattern<T, TKey>
        where TKey : notnull
    {

    }
}