namespace RepositoryPattern
{
    internal class InMemoryLongableStorage<T> : InMemoryStorage<T, long>, ILongableRepositoryPattern<T>
    {
        public InMemoryLongableStorage(RepositoryPatternInMemorySettingsFactory settings) : base(settings)
        {
        }
    }
}