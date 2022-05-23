namespace RepositoryPattern
{
    public interface IStringableRepositoryPattern<T> : IRepositoryPattern<T, string>, IStringableCommandPattern<T>, IStringableQueryPattern<T>
    {
    }
}