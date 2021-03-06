using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text;

namespace RepositoryPattern.Client
{
    internal class RepositoryPatternClient<T, TKey> : IRepositoryPatternClient<T, TKey>
        where TKey : notnull
    {
        private readonly HttpClient _httpClient;
        public RepositoryPatternClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient($"{typeof(T).Name}RepositoryPatternClient");
        }

        public Task<bool> DeleteAsync(TKey key, CancellationToken cancellationToken = default)
            => _httpClient.GetFromJsonAsync<bool>($"delete?key={key}", cancellationToken);

        public Task<T?> GetAsync(TKey key, CancellationToken cancellationToken = default)
            => _httpClient.GetFromJsonAsync<T>($"get?key={key}", cancellationToken);

        public async Task<bool> InsertAsync(TKey key, T value, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"insert?key={key}", value, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response!.Content.ReadFromJsonAsync<bool>(cancellationToken: cancellationToken);
        }

        public Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>>? predicate = null, int? top = null, int? skip = null, CancellationToken cancellationToken = default)
        {
            var query = new StringBuilder("list");
            if (predicate != null || top != null || skip != null)
                query.Append('?');
            if (predicate != null)
                query.Append($"query={predicate}");
            if (top != null)
                query.Append($"{(predicate == null ? string.Empty : "?")}top={top}");
            if (skip != null)
                query.Append($"{(predicate == null && top == null ? string.Empty : "?")}skip={skip}");
            return _httpClient.GetFromJsonAsync<IEnumerable<T>>(query.ToString(), cancellationToken)!;
        }

        public async Task<bool> UpdateAsync(TKey key, T value, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync($"update?key={key}", value, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>(cancellationToken: cancellationToken);
        }
    }
}