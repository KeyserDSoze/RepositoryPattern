namespace RepositoryPattern
{
    public class RepositoryPatternInMemorySettingsFactory
    {
        public static RepositoryPatternInMemorySettingsFactory Instance { get; } = new();
        public Dictionary<string, RepositoryPatternBehaviorSettings> Settings { get; } = new();
        private RepositoryPatternInMemorySettingsFactory() { }
    }
}