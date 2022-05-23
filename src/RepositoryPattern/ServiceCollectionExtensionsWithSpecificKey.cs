﻿using RepositoryPattern;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryPatternWithStringKey<T, TStorage>(this IServiceCollection services,
           ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
           where TStorage : class, IStringableRepositoryPattern<T>
               => services.AddRepositoryPattern<T, string, TStorage>();
        public static IServiceCollection AddCommandPatternWithStringKey<T, TStorage>(this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TStorage : class, ICommandPattern<T, string>
                => services.AddCommandPattern<T, string, TStorage>();
        public static IServiceCollection AddQueryPatternWithStringKey<T, TKey, TStorage>(this IServiceCollection services,
           ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
           where TStorage : class, IQueryPattern<T, string>
                => services.AddQueryPattern<T, string, TStorage>();
        public static RepositoryPatternInMemoryBuilder<T, string> AddRepositoryPatternInMemoryStorageWithStringKey<T>(
            this IServiceCollection services,
            Action<RepositoryPatternInMemorySettings> settings)
        => services.AddRepositoryPatternInMemoryStorage<T, string>(settings);
    }
}