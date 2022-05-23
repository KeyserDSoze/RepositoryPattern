namespace RepositoryPattern
{
    public interface IGuidableRepositoryPattern<T> : IGuidableCommandPattern<T>, IGuidableQueryPattern<T>
    {
    }
}