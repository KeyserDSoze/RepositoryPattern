namespace RepositoryPattern
{
    internal class InMemoryIntableStorage<T> : InMemoryStorage<T, int>, IIntableRepositoryPattern<T>
    {
        public InMemoryIntableStorage(RepositoryPatternInMemorySettingsFactory settings) : base(settings)
        {
        }
    }
}