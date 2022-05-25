using Microsoft.Extensions.DependencyInjection;
using RepositoryPattern.Data;
using RepositoryPattern.Utility;
using System.Linq.Expressions;

namespace RepositoryPattern
{
    public class RepositoryPatternInMemoryRegexBuilder<T, TKey>
        where TKey : notnull
    {
        private readonly IServiceCollection _services;
        private readonly RepositoryPatternInMemoryBuilder<T, TKey> _builder;
        public RepositoryPatternInMemoryRegexBuilder(IServiceCollection services,
            RepositoryPatternInMemoryBuilder<T, TKey> builder)
        {
            _services = services;
            _builder = builder;
        }
        public RepositoryPatternInMemoryRegexBuilder<T, TKey> WithPattern<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath, params string[] regex)
        {
            string nameOfProperty = string.Join(".", navigationPropertyPath.ToString().Split('.').Skip(1))
                .Replace("First().Value.", "Value.")
                .Replace("First().Key.", "Key.")
                .Replace("First().", string.Empty);
            var dictionary = RepositoryPatternInMemorySettingsFactory.Instance.Settings[Naming.Settings<T, TKey>()].RegexForValueCreation;
            if (dictionary.ContainsKey(nameOfProperty))
                dictionary[nameOfProperty] = regex;
            else
                dictionary.Add(nameOfProperty, regex);
            return this;
        }
        public RepositoryPatternInMemoryRegexBuilder<T, TKey> WithValue<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath, Func<TProperty> creator)
        {
            string nameOfProperty = string.Join(".", navigationPropertyPath.ToString().Split('.').Skip(1))
                .Replace("First().Value.", string.Empty)
                .Replace("First().Key.", string.Empty)
                .Replace("First().", string.Empty);
            var dictionary = RepositoryPatternInMemorySettingsFactory.Instance.Settings[Naming.Settings<T, TKey>()].DelegatedMethodForValueCreation;
            if (dictionary.ContainsKey(nameOfProperty))
                dictionary[nameOfProperty] = () => creator.Invoke()!;
            else
                dictionary.Add(nameOfProperty, () => creator.Invoke()!);
            return this;
        }
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