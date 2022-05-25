namespace RepositoryPattern.Data
{
    internal class ObjectPopulationService : IPopulationService
    {
        private readonly IRandomPopulationService _populationService;

        public ObjectPopulationService(IRandomPopulationService populationService)
        {
            _populationService = populationService;
        }

        public dynamic GetValue(Type type, int numberOfEntities, string treeName)
        {
            if (!type.IsInterface && !type.IsAbstract)
            {
                var entity = Activator.CreateInstance(type);
                try
                {
                    var properties = type.GetProperties();
                    foreach (var property in properties)
                    {
                        property.SetValue(entity, _populationService.Construct(property, numberOfEntities, treeName));
                    }
                }
                catch
                {
                }
                return entity!;
            }
            return default!;
        }
    }
}