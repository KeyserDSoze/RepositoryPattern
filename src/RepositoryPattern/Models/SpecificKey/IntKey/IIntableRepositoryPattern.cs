namespace RepositoryPattern
{
    public interface IIntableRepositoryPattern<T> : IIntableCommandPattern<T>, IIntableQueryPattern<T>
    {
    }
}