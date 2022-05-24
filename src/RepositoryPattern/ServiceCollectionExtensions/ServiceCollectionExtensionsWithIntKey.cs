using RepositoryPattern;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryPatternWithIntKey<T, TStorage>(this IServiceCollection services,
           ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
           where TStorage : class, IIntableRepositoryPattern<T>
               => services.AddRepositoryPattern<T, int, TStorage>(serviceLifetime);
        public static IServiceCollection AddCommandPatternWithIntKey<T, TStorage>(this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TStorage : class, IIntableCommandPattern<T>
                => services.AddCommandPattern<T, int, TStorage>(serviceLifetime);
        public static IServiceCollection AddQueryPatternWithIntKey<T, TKey, TStorage>(this IServiceCollection services,
           ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
           where TStorage : class, IIntableQueryPattern<T>
                => services.AddQueryPattern<T, int, TStorage>(serviceLifetime);
        public static RepositoryPatternInMemoryBuilder<T, int> AddRepositoryPatternInMemoryStorageWithIntKey<T>(
            this IServiceCollection services,
            Action<RepositoryPatternBehaviorSettings> settings)
        => services.AddRepositoryPatternInMemoryStorage<T, int>(settings);
    }
}