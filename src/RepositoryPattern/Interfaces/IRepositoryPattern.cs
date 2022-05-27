namespace RepositoryPattern
{
    public interface IRepositoryPattern<T, TKey> : ICommandPattern<T, TKey>, IQueryPattern<T, TKey>
        where TKey : notnull
    {
    }
}