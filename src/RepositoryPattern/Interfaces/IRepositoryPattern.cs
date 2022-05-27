namespace RepositoryPattern
{
    public interface IRepositoryPattern : ICommandPattern, IQueryPattern { }
    public interface IRepositoryPattern<T, TKey> : ICommandPattern<T, TKey>, IQueryPattern<T, TKey>, IRepositoryPattern
        where TKey : notnull
    {
    }
}