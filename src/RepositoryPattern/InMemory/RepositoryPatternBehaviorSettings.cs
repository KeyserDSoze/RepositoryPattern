﻿namespace RepositoryPattern
{
    public class RepositoryPatternBehaviorSettings
    {
        public Range MillisecondsOfWaitForDelete { get; set; }
        public Range MillisecondsOfWaitForInsert { get; set; }
        public Range MillisecondsOfWaitForUpdate { get; set; }
        public Range MillisecondsOfWaitForGet { get; set; }
        public Range MillisecondsOfWaitForWhere { get; set; }
        public List<ExceptionOdds> ExceptionOddsForDelete { get; } = new();
        public List<ExceptionOdds> ExceptionOddsForInsert { get; } = new();
        public List<ExceptionOdds> ExceptionOddsForUpdate { get; } = new();
        public List<ExceptionOdds> ExceptionOddsForGet { get; } = new();
        public List<ExceptionOdds> ExceptionOddsForWhere { get; } = new();
        public Dictionary<string, string[]> RegexForValueCreation { get; set; } = new();
        public Dictionary<string, Func<dynamic>> DelegatedMethodForValueCreation { get; set; } = new();
    }
}