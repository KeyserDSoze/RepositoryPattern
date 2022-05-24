using RepositoryPattern;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryPatternWithStringKey<T, TStorage>(this IServiceCollection services,
           ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
           where TStorage : class, IStringableRepositoryPattern<T>
               => services.AddRepositoryPattern<T, string, TStorage>(serviceLifetime);
        public static IServiceCollection AddCommandPatternWithStringKey<T, TStorage>(this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TStorage : class, IStringableCommandPattern<T>
                => services.AddCommandPattern<T, string, TStorage>(serviceLifetime);
        public static IServiceCollection AddQueryPatternWithStringKey<T, TKey, TStorage>(this IServiceCollection services,
           ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
           where TStorage : class, IStringableQueryPattern<T>
                => services.AddQueryPattern<T, string, TStorage>(serviceLifetime);
        public static RepositoryPatternInMemoryBuilder<T, string> AddRepositoryPatternInMemoryStorageWithStringKey<T>(
            this IServiceCollection services,
            Action<RepositoryPatternBehaviorSettings> settings)
        => services.AddRepositoryPatternInMemoryStorage<T, string>(settings);
    }
}