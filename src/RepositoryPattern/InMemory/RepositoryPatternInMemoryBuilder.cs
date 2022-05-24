﻿using Microsoft.Extensions.DependencyInjection;
using RepositoryPattern.Data;
using System.Linq.Expressions;

namespace RepositoryPattern
{
    public class RepositoryPatternInMemoryBuilder<T, TKey>
        where TKey : notnull
    {
        private readonly IServiceCollection _services;
        public RepositoryPatternInMemoryBuilder(IServiceCollection services)
            => _services = services;
        public RepositoryPatternInMemoryBuilder<TNext, TNextKey> AddRepositoryPatternInMemoryStorage<TNext, TNextKey>(Action<RepositoryPatternBehaviorSettings>? settings = default)
            where TNextKey : notnull
            => _services!.AddRepositoryPatternInMemoryStorage<TNext, TNextKey>(settings);
        public RepositoryPatternInMemoryBuilder<TNext, string> AddRepositoryPatternInMemoryStorageWithStringKey<TNext>(Action<RepositoryPatternBehaviorSettings>? settings = default)
            => _services!.AddRepositoryPatternInMemoryStorageWithStringKey<TNext>(settings);
        public RepositoryPatternInMemoryBuilder<TNext, Guid> AddRepositoryPatternInMemoryStorageWithGuidKey<TNext>(Action<RepositoryPatternBehaviorSettings>? settings = default)
            => _services!.AddRepositoryPatternInMemoryStorageWithGuidKey<TNext>(settings);
        public RepositoryPatternInMemoryBuilder<TNext, long> AddRepositoryPatternInMemoryStorageWithLongKey<TNext>(Action<RepositoryPatternBehaviorSettings>? settings = default)
            => _services!.AddRepositoryPatternInMemoryStorageWithLongKey<TNext>(settings);
        public RepositoryPatternInMemoryBuilder<TNext, int> AddRepositoryPatternInMemoryStorageWithIntKey<TNext>(Action<RepositoryPatternBehaviorSettings>? settings = default)
            => _services!.AddRepositoryPatternInMemoryStorageWithIntKey<TNext>(settings);
        public RepositoryPatternInMemoryBuilder<T, TKey> PopulateWithRandomData(Expression<Func<T, TKey>> navigationKey, int numberOfElements = 100, int numberOfElementsWhenEnumerableIsFound = 10)
        {
            var nameOfKey = navigationKey.ToString().Split('.').Last();
            var properties = typeof(T).GetProperties();
            for (int i = 0; i < numberOfElements; i++)
            {
                var entity = Activator.CreateInstance<T>();
                foreach (var property in properties)
                {
                    property.SetValue(entity, RandomPopulationService.Construct(property, numberOfElementsWhenEnumerableIsFound));
                }
                var key = properties.First(x => x.Name == nameOfKey).GetValue(entity);
                InMemoryStorage<T, TKey>._values.Add((TKey)key!, entity);
            }
            return this;
        }
        public IServiceCollection Finalize()
            => _services!;
    }
}