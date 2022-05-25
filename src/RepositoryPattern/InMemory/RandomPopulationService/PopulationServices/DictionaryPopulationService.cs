using System.Collections;

namespace RepositoryPattern.Data
{
    internal class DictionaryPopulationService : IPopulationService
    {
        private readonly IRandomPopulationService _populationService;

        public DictionaryPopulationService(IRandomPopulationService populationService)
        {
            _populationService = populationService;
        }

        public dynamic GetValue(Type type, int numberOfEntities, string treeName)
        {
            var keyType = type.GetGenericArguments().First();
            var valueType = type.GetGenericArguments().Last();
            var dictionaryType = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);
            var entity = Activator.CreateInstance(dictionaryType)! as IDictionary;
            for (int i = 0; i < numberOfEntities; i++)
            {
                var newKey = _populationService.Construct(type.GetGenericArguments().First(), numberOfEntities, treeName, "Key");
                var newValue = _populationService.Construct(type.GetGenericArguments().Last(), numberOfEntities, treeName, "Value");
                entity!.Add(newKey, newValue);
            }
            return entity!;
        }
    }
}