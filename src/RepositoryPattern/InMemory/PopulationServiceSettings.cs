namespace RepositoryPattern
{
    internal class PopulationServiceSettings
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Type PopulationServiceType { get; init; }
        public Type EntityType { get; init; }
        public Type InternalSettingsType { get; init; }
        public Action<object, object> AddElementToMemory { get; init; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string? KeyName { get; init; }
        public int NumberOfElements { get; init; }
        public int NumberOfElementsWhenEnumerableIsFound { get; init; }
    }
}