namespace RepositoryPattern
{
    public interface ILongableRepositoryPattern<T> : ILongableCommandPattern<T>, ILongableQueryPattern<T>
    {
    }
}