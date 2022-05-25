using Microsoft.Extensions.DependencyInjection;

namespace RepositoryPattern
{
    public class RepositoryPatternInMemoryBuilder<T, TKey>
        where TKey : notnull
    {
        private readonly IServiceCollection _services;
        public RepositoryPatternInMemoryBuilder(IServiceCollection services)
            => _services = services;
        public RepositoryPatternInMemoryBuilder<TNext, TNextKey> AddRepositoryPatternInMemoryStorage<TNext, TNextKey>(Action<RepositoryPatternBehaviorSettings>? settings = default)
            where TNextKey : notnull
            => _services!.AddRepositoryPatternInMemoryStorage<TNext, TNextKey>(settings);
        public RepositoryPatternInMemoryBuilder<TNext, string> AddRepositoryPatternInMemoryStorageWithStringKey<TNext>(Action<RepositoryPatternBehaviorSettings>? settings = default)
            => _services!.AddRepositoryPatternInMemoryStorageWithStringKey<TNext>(settings);
        public RepositoryPatternInMemoryBuilder<TNext, Guid> AddRepositoryPatternInMemoryStorageWithGuidKey<TNext>(Action<RepositoryPatternBehaviorSettings>? settings = default)
            => _services!.AddRepositoryPatternInMemoryStorageWithGuidKey<TNext>(settings);
        public RepositoryPatternInMemoryBuilder<TNext, long> AddRepositoryPatternInMemoryStorageWithLongKey<TNext>(Action<RepositoryPatternBehaviorSettings>? settings = default)
            => _services!.AddRepositoryPatternInMemoryStorageWithLongKey<TNext>(settings);
        public RepositoryPatternInMemoryBuilder<TNext, int> AddRepositoryPatternInMemoryStorageWithIntKey<TNext>(Action<RepositoryPatternBehaviorSettings>? settings = default)
            => _services!.AddRepositoryPatternInMemoryStorageWithIntKey<TNext>(settings);
        public RepositoryPatternInMemoryCreatorBuilder<T, TKey> PopulateWithRandomData() 
            => new(_services, this);
        public IServiceCollection Finalize()
            => _services!;
    }
}