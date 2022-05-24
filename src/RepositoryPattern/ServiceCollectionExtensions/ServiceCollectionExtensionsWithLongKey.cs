using RepositoryPattern;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryPatternWithLongKey<T, TStorage>(this IServiceCollection services,
           ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
           where TStorage : class, ILongableRepositoryPattern<T>
               => services.AddRepositoryPattern<T, long, TStorage>(serviceLifetime);
        public static IServiceCollection AddCommandPatternWithLongKey<T, TStorage>(this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TStorage : class, ILongableCommandPattern<T>
                => services.AddCommandPattern<T, long, TStorage>(serviceLifetime);
        public static IServiceCollection AddQueryPatternWithLongKey<T, TKey, TStorage>(this IServiceCollection services,
           ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
           where TStorage : class, ILongableQueryPattern<T>
                => services.AddQueryPattern<T, long, TStorage>(serviceLifetime);
        public static RepositoryPatternInMemoryBuilder<T, long> AddRepositoryPatternInMemoryStorageWithLongKey<T>(
            this IServiceCollection services,
            Action<RepositoryPatternBehaviorSettings>? settings = default)
        => services.AddRepositoryPatternInMemoryStorage<T, long>(settings);
    }
}