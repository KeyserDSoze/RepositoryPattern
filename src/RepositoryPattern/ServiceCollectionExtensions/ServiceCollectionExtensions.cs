using RepositoryPattern;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        private static IServiceCollection AddServiceWithLifeTime<TInterface, TImplementation>(this IServiceCollection services,
          ServiceLifetime serviceLifetime)
            where TImplementation : class, TInterface
        => serviceLifetime switch
        {
            ServiceLifetime.Scoped => services.AddScoped(typeof(TInterface), typeof(TImplementation)),
            ServiceLifetime.Transient => services.AddTransient(typeof(TInterface), typeof(TImplementation)),
            ServiceLifetime.Singleton => services.AddSingleton(typeof(TInterface), typeof(TImplementation)),
            _ => services.AddScoped(typeof(TInterface), typeof(TImplementation))
        };
        public static IServiceCollection AddRepositoryPattern<T, TKey, TStorage>(this IServiceCollection services,
          ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
          where TStorage : class, IRepositoryPattern<T, TKey>
          where TKey : notnull
              => services.AddServiceWithLifeTime<IRepositoryPattern<T, TKey>, TStorage>(serviceLifetime);
        public static IServiceCollection AddCommandPattern<T, TKey, TStorage>(this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TStorage : class, ICommandPattern<T, TKey>
            where TKey : notnull
              => services.AddServiceWithLifeTime<ICommandPattern<T, TKey>, TStorage>(serviceLifetime);
        public static IServiceCollection AddQueryPattern<T, TKey, TStorage>(this IServiceCollection services,
           ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
           where TStorage : class, IQueryPattern<T, TKey>
           where TKey : notnull
               => services.AddServiceWithLifeTime<IQueryPattern<T, TKey>, TStorage>(serviceLifetime);
    }
}