namespace RepositoryPattern.Data
{
    internal class AbstractPopulationService : IPopulationService
    {
        private readonly IRandomPopulationService _populationService;
        public AbstractPopulationService(IRandomPopulationService populationService)
        {
            _populationService = populationService;
        }
        public dynamic GetValue(Type type, int numberOfEntities, string treeName)
        {
            return null!;
        }
    }
}