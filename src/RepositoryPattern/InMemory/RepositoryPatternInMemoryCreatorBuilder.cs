using Microsoft.Extensions.DependencyInjection;
using RepositoryPattern.Data;
using RepositoryPattern.Utility;
using System.Collections;
using System.Linq.Expressions;

namespace RepositoryPattern
{
    public class RepositoryPatternInMemoryCreatorBuilder<T, TKey>
        where TKey : notnull
    {
        private readonly IServiceCollection _services;
        private readonly RepositoryPatternInMemoryBuilder<T, TKey> _builder;
        public RepositoryPatternInMemoryCreatorBuilder(IServiceCollection services,
            RepositoryPatternInMemoryBuilder<T, TKey> builder)
        {
            _services = services;
            _builder = builder;
        }
        private static string GetNameOfProperty<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath)
            => string.Join(".", navigationPropertyPath.ToString().Split('.').Skip(1))
                .Replace("First().Value.", "Value.")
                .Replace("First().Key.", "Key.")
                .Replace("First().", string.Empty);
        public RepositoryPatternInMemoryCreatorBuilder<T, TKey> WithPattern<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath, params string[] regex)
        {
            string nameOfProperty = GetNameOfProperty(navigationPropertyPath);
            var dictionary = RepositoryPatternInMemorySettingsFactory.Instance.Settings[Naming.Settings<T, TKey>()].RegexForValueCreation;
            if (dictionary.ContainsKey(nameOfProperty))
                dictionary[nameOfProperty] = regex;
            else
                dictionary.Add(nameOfProperty, regex);
            return this;
        }
        public RepositoryPatternInMemoryCreatorBuilder<T, TKey> WithSpecificNumberOfElements<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath, int numberOfElements) 
        {
            string nameOfProperty = GetNameOfProperty(navigationPropertyPath);
            var dictionary = RepositoryPatternInMemorySettingsFactory.Instance.Settings[Naming.Settings<T, TKey>()].NumberOfElements;
            if (dictionary.ContainsKey(nameOfProperty))
                dictionary[nameOfProperty] = numberOfElements;
            else
                dictionary.Add(nameOfProperty, numberOfElements);
            return this;
        }
        public RepositoryPatternInMemoryCreatorBuilder<T, TKey> WithValue<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath, Func<TProperty> creator)
        {
            string nameOfProperty = GetNameOfProperty(navigationPropertyPath);
            var dictionary = RepositoryPatternInMemorySettingsFactory.Instance.Settings[Naming.Settings<T, TKey>()].DelegatedMethodForValueCreation;
            if (dictionary.ContainsKey(nameOfProperty))
                dictionary[nameOfProperty] = () => creator.Invoke()!;
            else
                dictionary.Add(nameOfProperty, () => creator.Invoke()!);
            return this;
        }
        public RepositoryPatternInMemoryCreatorBuilder<T, TKey> WithImplementation<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath, Type implementationType)
        {
            string nameOfProperty = GetNameOfProperty(navigationPropertyPath);
            var dictionary = RepositoryPatternInMemorySettingsFactory.Instance.Settings[Naming.Settings<T, TKey>()].ImplementationForValueCreation;
            if (dictionary.ContainsKey(nameOfProperty))
                dictionary[nameOfProperty] = implementationType;
            else
                dictionary.Add(nameOfProperty, implementationType);
            return this;
        }
        public RepositoryPatternInMemoryCreatorBuilder<T, TKey> WithImplementation<TProperty, TEntity>(Expression<Func<T, TProperty>> navigationPropertyPath)
            => WithImplementation(navigationPropertyPath, typeof(TEntity));
        public RepositoryPatternInMemoryBuilder<T, TKey> Populate(Expression<Func<T, TKey>> navigationKey, int numberOfElements = 100, int numberOfElementsWhenEnumerableIsFound = 10)
        {
            var nameOfKey = navigationKey.ToString().Split('.').Last();
            var properties = typeof(T).GetProperties();
            IRandomPopulationService populationService = new RandomPopulationService<T, TKey>();
            for (int i = 0; i < numberOfElements; i++)
            {
                var entity = Activator.CreateInstance<T>();
                foreach (var property in properties)
                {
                    //property.SetValue(entity, RandomPopulationService<T, TKey>.Construct(property, numberOfElementsWhenEnumerableIsFound, string.Empty));
                    property.SetValue(entity, populationService.Construct(property, numberOfElementsWhenEnumerableIsFound, string.Empty));
                }
                var key = properties.First(x => x.Name == nameOfKey).GetValue(entity);
                InMemoryStorage<T, TKey>._values.Add((TKey)key!, entity);
            }
            return _builder;
        }
    }
}