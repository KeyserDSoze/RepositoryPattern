using RepositoryPattern;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
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
    }
}