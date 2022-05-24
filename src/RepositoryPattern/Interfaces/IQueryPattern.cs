namespace RepositoryPattern
{
    public interface IQueryPattern<T, TKey>
        where TKey : notnull
    {
        Task<T?> GetAsync(TKey key);
        Task<List<T>> ToListAsync(Func<T, bool>? predicate = null);
    }
}