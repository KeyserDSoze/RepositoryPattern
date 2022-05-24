namespace RepositoryPattern
{
    public interface IIntableRepositoryPattern<T> : IRepositoryPattern<T, int>, IIntableCommandPattern<T>, IIntableQueryPattern<T>
    {
    }
}