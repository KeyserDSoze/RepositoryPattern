namespace RepositoryPattern.Data
{
    internal class GuidPopulationService : IPopulationService
    {
        public dynamic GetValue(Type type, int numberOfEntities, string treeName)
            => Guid.NewGuid();
    }
}