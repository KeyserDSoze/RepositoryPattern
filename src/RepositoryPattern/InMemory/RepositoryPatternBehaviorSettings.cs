namespace RepositoryPattern
{
    public class RepositoryPatternBehaviorSettings
    {
        public Range MillisecondsOfWaitForDelete { get; set; }
        public Range MillisecondsOfWaitForInsert { get; set; }
        public Range MillisecondsOfWaitForUpdate { get; set; }
        public Range MillisecondsOfWaitForGet { get; set; }
        public Range MillisecondsOfWaitForQuery { get; set; }
        public Range MillisecondsOfWaitBeforeExceptionForDelete { get; set; }
        public Range MillisecondsOfWaitBeforeExceptionForInsert { get; set; }
        public Range MillisecondsOfWaitBeforeExceptionForUpdate { get; set; }
        public Range MillisecondsOfWaitBeforeExceptionForGet { get; set; }
        public Range MillisecondsOfWaitBeforeExceptionForQuery { get; set; }
        public List<ExceptionOdds> ExceptionOddsForDelete { get; } = new();
        public List<ExceptionOdds> ExceptionOddsForInsert { get; } = new();
        public List<ExceptionOdds> ExceptionOddsForUpdate { get; } = new();
        public List<ExceptionOdds> ExceptionOddsForGet { get; } = new();
        public List<ExceptionOdds> ExceptionOddsForQuery { get; } = new();
        public Dictionary<string, string[]> RegexForValueCreation { get; set; } = new();
        public Dictionary<string, Func<dynamic>> DelegatedMethodForValueCreation { get; set; } = new();
        public Dictionary<string, Type> ImplementationForValueCreation { get; set; } = new();
        public Dictionary<string, int> NumberOfElements { get; set; } = new();
    }
}