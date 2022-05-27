using RepositoryPattern;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        private static bool IsThatInterface<TEntity, TInterface>()
            => typeof(TEntity).GetInterfaces().Any(x => x == typeof(TInterface));
        private static Type GetTypeOfModel<TEntity>()
            => typeof(TEntity).GetGenericArguments()[0]!;
        private static Type GetTypeOfKey<TEntity>()
        {
            var arguments = typeof(TEntity).GetGenericArguments();
            if (arguments.Length > 1)
                return arguments[1];
            else
                return typeof(TEntity).GetInterfaces().First().GetGenericArguments()[1]!;
        }

        private static IServiceCollection AddServiceWithLifeTime<TInterface, TImplementation>(this IServiceCollection services,
          ServiceLifetime serviceLifetime)
            where TImplementation : class, TInterface
        {
            var entityType = GetTypeOfModel<TInterface>();
            var keyType = GetTypeOfKey<TInterface>();

            if (!WebApplicationExtensions.Services.ContainsKey(entityType))
                WebApplicationExtensions.Services.Add(entityType, new());
            WebApplicationExtensions.Services[entityType].KeyType = keyType;

            if (IsThatInterface<TInterface, IRepositoryPattern>())
                WebApplicationExtensions.Services[entityType].RepositoryType = typeof(TInterface);
            else if (IsThatInterface<TInterface, ICommandPattern>())
                WebApplicationExtensions.Services[entityType].CommandType = typeof(TInterface);
            else if (IsThatInterface<TInterface, IQueryPattern>())
                WebApplicationExtensions.Services[entityType].QueryType = typeof(TInterface);

            return serviceLifetime switch
            {
                ServiceLifetime.Scoped => services.AddScoped(typeof(TInterface), typeof(TImplementation)),
                ServiceLifetime.Transient => services.AddTransient(typeof(TInterface), typeof(TImplementation)),
                ServiceLifetime.Singleton => services.AddSingleton(typeof(TInterface), typeof(TImplementation)),
                _ => services.AddScoped(typeof(TInterface), typeof(TImplementation))
            };
        }

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