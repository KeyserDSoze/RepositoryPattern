namespace RepositoryPattern.Data
{
    internal class ImplementationPopulationService : IPopulationService
    {
        private readonly Type _implementationType;
        private readonly IRandomPopulationService _populationService;

        public ImplementationPopulationService(Type implementationType, IRandomPopulationService populationService)
        {
            _implementationType = implementationType;
            _populationService = populationService;
        }

        public dynamic GetValue(Type type, int numberOfEntities, string treeName)
            => _populationService.Construct(_implementationType, numberOfEntities, treeName, string.Empty)!;
    }
}