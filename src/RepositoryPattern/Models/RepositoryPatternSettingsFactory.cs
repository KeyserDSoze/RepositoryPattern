namespace RepositoryPattern
{
    public class RepositoryPatternSettingsFactory
    {
        public static RepositoryPatternSettingsFactory Instance { get; } = new();
        public Dictionary<string, RepositoryPatternInMemorySettings> Settings { get; } = new();
        private RepositoryPatternSettingsFactory() { }
    }
}