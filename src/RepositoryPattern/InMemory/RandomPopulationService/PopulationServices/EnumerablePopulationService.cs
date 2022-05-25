using System.Collections;

namespace RepositoryPattern.Data
{
    internal class EnumerablePopulationService : IPopulationService
    {
        private readonly IRandomPopulationService _populationService;

        public EnumerablePopulationService(IRandomPopulationService populationService)
        {
            _populationService = populationService;
        }

        public dynamic GetValue(Type type, int numberOfEntities, string treeName)
        {
            var valueType = type.GetGenericArguments().First();
            var listType = typeof(List<>).MakeGenericType(valueType);
            var entity = Activator.CreateInstance(listType)! as IList;
            for (int i = 0; i < numberOfEntities; i++)
            {
                var newValue = _populationService.Construct(type.GetGenericArguments().First(), numberOfEntities, treeName, string.Empty);
                entity!.Add(newValue);
            }
            return entity!;
        }
    }
}