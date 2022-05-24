namespace RepositoryPattern
{
    public interface IGuidableRepositoryPattern<T> : IRepositoryPattern<T, Guid>, IGuidableCommandPattern<T>, IGuidableQueryPattern<T>
    {
    }
}