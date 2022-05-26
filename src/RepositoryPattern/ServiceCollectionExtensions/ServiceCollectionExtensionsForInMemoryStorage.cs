using RepositoryPattern;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static RepositoryPatternInMemoryBuilder<T, TKey> AddRepositoryPatternInMemoryStorage<T, TKey>(
            this IServiceCollection services,
            Action<RepositoryPatternBehaviorSettings>? settings = default)
            where TKey : notnull
        {
            var options = new RepositoryPatternBehaviorSettings();
            settings?.Invoke(options);
            Check(options.ExceptionOddsForQuery);
            Check(options.ExceptionOddsForInsert);
            Check(options.ExceptionOddsForUpdate);
            Check(options.ExceptionOddsForGet);
            Check(options.ExceptionOddsForDelete);
            RepositoryPatternInMemorySettingsFactory.Instance.Settings.Add(typeof(IRepositoryPattern<T, TKey>).FullName!, options);
            services.AddSingleton(RepositoryPatternInMemorySettingsFactory.Instance);
            services.AddSingleton<IRepositoryPattern<T, TKey>, InMemoryStorage<T, TKey>>();
            services.AddSingleton<ICommandPattern<T, TKey>, InMemoryStorage<T, TKey>>();
            services.AddSingleton<IQueryPattern<T, TKey>, InMemoryStorage<T, TKey>>();
            if (typeof(TKey) == typeof(string))
            {
                services.AddSingleton<IStringableRepositoryPattern<T>, InMemoryStringableStorage<T>>();
                services.AddSingleton<IStringableCommandPattern<T>, InMemoryStringableStorage<T>>();
                services.AddSingleton<IStringableQueryPattern<T>, InMemoryStringableStorage<T>>();
            }
            else if (typeof(TKey) == typeof(int))
            {
                services.AddSingleton<IIntableRepositoryPattern<T>, InMemoryIntableStorage<T>>();
                services.AddSingleton<IIntableCommandPattern<T>, InMemoryIntableStorage<T>>();
                services.AddSingleton<IIntableQueryPattern<T>, InMemoryIntableStorage<T>>();
            }
            else if (typeof(TKey) == typeof(long))
            {
                services.AddSingleton<ILongableRepositoryPattern<T>, InMemoryLongableStorage<T>>();
                services.AddSingleton<ILongableCommandPattern<T>, InMemoryLongableStorage<T>>();
                services.AddSingleton<ILongableQueryPattern<T>, InMemoryLongableStorage<T>>();
            }
            else if (typeof(TKey) == typeof(Guid))
            {
                services.AddSingleton<IGuidableRepositoryPattern<T>, InMemoryGuidableStorage<T>>();
                services.AddSingleton<IGuidableCommandPattern<T>, InMemoryGuidableStorage<T>>();
                services.AddSingleton<IGuidableRepositoryPattern<T>, InMemoryGuidableStorage<T>>();
            }

            return new RepositoryPatternInMemoryBuilder<T, TKey>(services);

            static void Check(List<ExceptionOdds> odds)
            {
                var total = odds.Sum(x => x.Percentage);
                if (odds.Where(x => x.Percentage <= 0 || x.Percentage > 100).Any())
                {
                    throw new ArgumentException("Some percentages are wrong, greater than 100% or lesser than 0.");
                }
                if (total > 100)
                    throw new ArgumentException("Your total percentage is greater than 100.");
            }
        }
    }
}