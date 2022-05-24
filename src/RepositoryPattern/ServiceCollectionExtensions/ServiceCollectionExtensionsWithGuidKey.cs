using RepositoryPattern;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryPatternWithGuidKey<T, TStorage>(this IServiceCollection services,
           ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
           where TStorage : class, IGuidableRepositoryPattern<T>
               => services.AddRepositoryPattern<T, Guid, TStorage>(serviceLifetime);
        public static IServiceCollection AddCommandPatternWithGuidKey<T, TStorage>(this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TStorage : class, IGuidableCommandPattern<T>
                => services.AddCommandPattern<T, Guid, TStorage>(serviceLifetime);
        public static IServiceCollection AddQueryPatternWithGuidKey<T, TKey, TStorage>(this IServiceCollection services,
           ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
           where TStorage : class, IGuidableQueryPattern<T>
                => services.AddQueryPattern<T, Guid, TStorage>(serviceLifetime);
        public static RepositoryPatternInMemoryBuilder<T, Guid> AddRepositoryPatternInMemoryStorageWithGuidKey<T>(
            this IServiceCollection services,
            Action<RepositoryPatternBehaviorSettings> settings)
        => services.AddRepositoryPatternInMemoryStorage<T, Guid>(settings);
    }
}