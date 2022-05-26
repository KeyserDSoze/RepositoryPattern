namespace RepositoryPattern
{
    public interface IQueryPattern<T, TKey>
        where TKey : notnull
    {
        Task<T?> GetAsync(TKey key);
        Task<IEnumerable<T>> QueryAsync(Func<T, bool>? predicate = null, int top = 0, int skip = 0);
    }
}