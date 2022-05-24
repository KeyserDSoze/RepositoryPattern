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
            string nameOfProperty = string.Join(".", navigationPropertyPath.ToString().Split('.').Skip(1));
            var dictionary = RepositoryPatternInMemorySettingsFactory.Instance.Settings[Naming.Settings<T, TKey>()].RegexForValueCreation;
            if (dictionary.ContainsKey(nameOfProperty))
                dictionary[nameOfProperty] = regex;
            else
                dictionary.Add(nameOfProperty, regex);
            return this;
        }
        public RepositoryPatternInMemoryBuilder<T, TKey> Populate(Expression<Func<T, TKey>> navigationKey, int numberOfElements = 100, int numberOfElementsWhenEnumerableIsFound = 10)
        {
            var nameOfKey = navigationKey.ToString().Split('.').Last();
            var properties = typeof(T).GetProperties();
            for (int i = 0; i < numberOfElements; i++)
            {
                var entity = Activator.CreateInstance<T>();
                foreach (var property in properties)
                {
                    property.SetValue(entity, RandomPopulationService<T, TKey>.Construct(property, numberOfElementsWhenEnumerableIsFound, string.Empty));
                }
                var key = properties.First(x => x.Name == nameOfKey).GetValue(entity);
                InMemoryStorage<T, TKey>._values.Add((TKey)key!, entity);
            }
            return _builder;
        }
    }
}