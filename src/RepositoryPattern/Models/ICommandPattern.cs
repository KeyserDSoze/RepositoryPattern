namespace RepositoryPattern
{
    public interface ICommandPattern<T, TKey>
        where TKey : notnull
    {
        Task<bool> InsertAsync(TKey key, T value);
        Task<bool> UpdateAsync(TKey key, T value);
        Task<bool> DeleteAsync(TKey key);
    }
}