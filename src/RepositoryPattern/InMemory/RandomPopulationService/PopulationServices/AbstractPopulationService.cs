namespace RepositoryPattern.Data
{
    internal class AbstractPopulationService : IPopulationService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "For future versions")]
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