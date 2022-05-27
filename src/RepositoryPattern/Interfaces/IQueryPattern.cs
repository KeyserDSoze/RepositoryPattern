using System.Linq.Expressions;

namespace RepositoryPattern
{
    public interface IQueryPattern { }
    public interface IQueryPattern<T, TKey> : IQueryPattern
        where TKey : notnull
    {
        Task<T?> GetAsync(TKey key);
        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>>? predicate = null, int? top = null, int? skip = null);
    }
}