using RepositoryPattern;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryPatternWithStringKey<T, TStorage>(this IServiceCollection services,
           ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
           where TStorage : class, IStringableRepositoryPattern<T>
               => services
                    .AddServiceWithLifeTime<IStringableRepositoryPattern<T>, TStorage>(serviceLifetime)
                    .AddRepositoryPattern<T, string, TStorage>(serviceLifetime);
        public static IServiceCollection AddCommandPatternWithStringKey<T, TStorage>(this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TStorage : class, IStringableCommandPattern<T>
                => services
                    .AddServiceWithLifeTime<IStringableCommandPattern<T>, TStorage>(serviceLifetime)
                    .AddCommandPattern<T, string, TStorage>(serviceLifetime);
        public static IServiceCollection AddQueryPatternWithStringKey<T, TKey, TStorage>(this IServiceCollection services,
           ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
           where TStorage : class, IStringableQueryPattern<T>
                => services
                    .AddServiceWithLifeTime<IStringableQueryPattern<T>, TStorage>(serviceLifetime)
                    .AddQueryPattern<T, string, TStorage>(serviceLifetime);
        public static RepositoryPatternInMemoryBuilder<T, string> AddRepositoryPatternInMemoryStorageWithStringKey<T>(
            this IServiceCollection services,
            Action<RepositoryPatternBehaviorSettings<T, string>>? settings = default)
        => services.AddRepositoryPatternInMemoryStorage(settings);
    }
}