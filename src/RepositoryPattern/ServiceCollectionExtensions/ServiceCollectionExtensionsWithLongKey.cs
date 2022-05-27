using RepositoryPattern;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryPatternWithLongKey<T, TStorage>(this IServiceCollection services,
           ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
           where TStorage : class, ILongableRepositoryPattern<T>
               => services
                    .AddServiceWithLifeTime<ILongableRepositoryPattern<T>, TStorage>(serviceLifetime)
                    .AddRepositoryPattern<T, long, TStorage>(serviceLifetime);
        public static IServiceCollection AddCommandPatternWithLongKey<T, TStorage>(this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TStorage : class, ILongableCommandPattern<T>
                => services
                    .AddServiceWithLifeTime<ILongableCommandPattern<T>, TStorage>(serviceLifetime)
                    .AddCommandPattern<T, long, TStorage>(serviceLifetime);
        public static IServiceCollection AddQueryPatternWithLongKey<T, TKey, TStorage>(this IServiceCollection services,
           ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
           where TStorage : class, ILongableQueryPattern<T>
                => services
                    .AddServiceWithLifeTime<ILongableQueryPattern<T>, TStorage>(serviceLifetime)
                    .AddQueryPattern<T, long, TStorage>(serviceLifetime);
        public static RepositoryPatternInMemoryBuilder<T, long> AddRepositoryPatternInMemoryStorageWithLongKey<T>(
            this IServiceCollection services,
            Action<RepositoryPatternBehaviorSettings<T, long>>? settings = default)
        => services.AddRepositoryPatternInMemoryStorage(settings);
    }
}