namespace RepositoryPattern
{
    internal class InMemoryStringableStorage<T> : InMemoryStorage<T, string>, IStringableRepositoryPattern<T>
    {
        public InMemoryStringableStorage(RepositoryPatternInMemorySettingsFactory settings) : base(settings)
        {
        }
    }
}