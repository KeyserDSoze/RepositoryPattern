namespace RepositoryPattern.Data
{
    internal class ArrayPopulationService : IPopulationService
    {
        private readonly IRandomPopulationService _populationService;

        public ArrayPopulationService(IRandomPopulationService populationService)
        {
            _populationService = populationService;
        }

        public dynamic GetValue(Type type, int numberOfEntities, string treeName)
        {
            var entity = Activator.CreateInstance(type, numberOfEntities);
            var valueType = type.GetElementType();
            for (int i = 0; i < numberOfEntities; i++)
                (entity as dynamic)![i] = _populationService.Construct(valueType!, numberOfEntities, treeName, string.Empty);
            return entity!;
        }
    }
}