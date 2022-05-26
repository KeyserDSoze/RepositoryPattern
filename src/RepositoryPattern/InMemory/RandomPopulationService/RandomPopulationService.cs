﻿using RepositoryPattern.Utility;
using System.Reflection;

namespace RepositoryPattern.Data
{
    internal class RandomPopulationService<T, TKey> : IRandomPopulationService
        where TKey : notnull
    {
        private readonly PopulationServiceFactory<T, TKey> _factory = new();
        public dynamic? Construct(PropertyInfo propertyInfo, int numberOfEntities, string treeName)
        {
            Type type = propertyInfo.PropertyType;
            return Construct(type, numberOfEntities, treeName, propertyInfo.Name);
        }
        public dynamic? Construct(Type type, int numberOfEntities, string treeName, string propertyName)
        {
            treeName = string.IsNullOrWhiteSpace(treeName) ? propertyName : (string.IsNullOrWhiteSpace(propertyName) ? treeName : $"{treeName}.{propertyName}");
            var service = _factory.GetService(type, this, treeName);
            if (service != default)
            {
                int? overridedNumberOfEntities = null;
                var numberOfEntitiesDictionary = RepositoryPatternInMemorySettingsFactory.Instance.Settings[Naming.Settings<T, TKey>()].NumberOfElements;
                if (numberOfEntitiesDictionary.ContainsKey(treeName))
                    overridedNumberOfEntities = numberOfEntitiesDictionary[treeName];
                return service.GetValue(type, overridedNumberOfEntities ?? numberOfEntities, treeName);
            }
            return default;
        }
    }
}