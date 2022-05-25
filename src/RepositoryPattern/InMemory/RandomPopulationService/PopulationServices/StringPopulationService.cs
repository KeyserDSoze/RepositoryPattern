namespace RepositoryPattern.Data
{
    internal class StringPopulationService : IPopulationService
    {
        public dynamic GetValue(Type type, int numberOfEntities, string treeName)
            => $"{treeName}_{Guid.NewGuid()}";
    }
}