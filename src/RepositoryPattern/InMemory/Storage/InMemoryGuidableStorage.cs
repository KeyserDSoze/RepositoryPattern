namespace RepositoryPattern
{
    internal class InMemoryGuidableStorage<T> : InMemoryStorage<T, Guid>, IGuidableRepositoryPattern<T>
    {
        public InMemoryGuidableStorage(RepositoryPatternInMemorySettingsFactory settings) : base(settings)
        {
        }
    }
}