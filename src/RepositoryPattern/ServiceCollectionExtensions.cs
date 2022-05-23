using RepositoryPattern;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryPattern<T, TKey, TStorage>(this IServiceCollection services,
          ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
          where TStorage : class, IRepositoryPattern<T, TKey>
          where TKey : notnull
              => serviceLifetime switch
              {
                  ServiceLifetime.Scoped => services.AddScoped<IRepositoryPattern<T, TKey>, TStorage>(),
                  ServiceLifetime.Transient => services.AddTransient<IRepositoryPattern<T, TKey>, TStorage>(),
                  ServiceLifetime.Singleton => services.AddSingleton<IRepositoryPattern<T, TKey>, TStorage>(),
                  _ => services.AddScoped<IRepositoryPattern<T, TKey>, TStorage>()
              };
        public static IServiceCollection AddCommandPattern<T, TKey, TStorage>(this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TStorage : class, ICommandPattern<T, TKey>
            where TKey : notnull
                => serviceLifetime switch
                {
                    ServiceLifetime.Scoped => services.AddScoped<ICommandPattern<T, TKey>, TStorage>(),
                    ServiceLifetime.Transient => services.AddTransient<ICommandPattern<T, TKey>, TStorage>(),
                    ServiceLifetime.Singleton => services.AddSingleton<ICommandPattern<T, TKey>, TStorage>(),
                    _ => services.AddScoped<ICommandPattern<T, TKey>, TStorage>()
                };
        public static IServiceCollection AddQueryPattern<T, TKey, TStorage>(this IServiceCollection services,
           ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
           where TStorage : class, IQueryPattern<T, TKey>
           where TKey : notnull
                => serviceLifetime switch
                {
                    ServiceLifetime.Scoped => services.AddScoped<IQueryPattern<T, TKey>, TStorage>(),
                    ServiceLifetime.Transient => services.AddTransient<IQueryPattern<T, TKey>, TStorage>(),
                    ServiceLifetime.Singleton => services.AddSingleton<IQueryPattern<T, TKey>, TStorage>(),
                    _ => services.AddScoped<IQueryPattern<T, TKey>, TStorage>()
                };
        public static RepositoryPatternInMemoryBuilder<T, TKey> AddRepositoryPatternInMemoryStorage<T, TKey>(
            this IServiceCollection services,
            Action<RepositoryPatternInMemorySettings> settings)
            where TKey : notnull
        {
            var options = new RepositoryPatternInMemorySettings();
            settings.Invoke(options);
            Check(options.ExceptionOddsForWhere);
            Check(options.ExceptionOddsForInsert);
            Check(options.ExceptionOddsForUpdate);
            Check(options.ExceptionOddsForGet);
            Check(options.ExceptionOddsForDelete);
            RepositoryPatternSettingsFactory.Instance.Settings.Add(nameof(IRepositoryPattern<T, TKey>), options);
            services.AddSingleton(RepositoryPatternSettingsFactory.Instance);
            services.AddSingleton<IRepositoryPattern<T, TKey>, InMemoryStorage<T, TKey>>();
            services.AddSingleton<ICommandPattern<T, TKey>, InMemoryStorage<T, TKey>>();
            services.AddSingleton<IQueryPattern<T, TKey>, InMemoryStorage<T, TKey>>();
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