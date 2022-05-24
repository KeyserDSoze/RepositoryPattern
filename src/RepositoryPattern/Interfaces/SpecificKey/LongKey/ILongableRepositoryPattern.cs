namespace RepositoryPattern
{
    public interface ILongableRepositoryPattern<T> : IRepositoryPattern<T, long>, ILongableCommandPattern<T>, ILongableQueryPattern<T>
    {
    }
}