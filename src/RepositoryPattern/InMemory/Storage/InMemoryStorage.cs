using System.Security.Cryptography;

namespace RepositoryPattern
{
    internal class InMemoryStorage<T, TKey> : IRepositoryPattern<T, TKey>
        where TKey : notnull
    {
        private readonly RepositoryPatternBehaviorSettings<T, TKey> _settings;

        public InMemoryStorage(RepositoryPatternBehaviorSettings<T, TKey> settings)
        {
            _settings = settings;
        }
        internal static Dictionary<TKey, T> Values { get; } = new();
        private static int GetRandomNumber(Range range)
        {
            int maxPlusOne = range.End.Value + 1 - range.Start.Value;
            return RandomNumberGenerator.GetInt32(maxPlusOne) + range.Start.Value;
        }
        private static Exception? GetException(List<ExceptionOdds> odds)
        {
            if (odds.Count == 0)
                return default;
            var oddBase = (int)Math.Pow(10, odds.Select(x => x.Percentage.ToString()).OrderByDescending(x => x.Length).First().Split('.').Last().Length);
            List<ExceptionOdds> normalizedOdds = new();
            foreach (var odd in odds)
            {
                normalizedOdds.Add(new ExceptionOdds
                {
                    Exception = odd.Exception,
                    Percentage = odd.Percentage * oddBase
                });
            }
            Range range = new(0, 100 * oddBase);
            var result = GetRandomNumber(range);
            int total = 0;
            foreach (var odd in normalizedOdds)
            {
                var value = (int)odd.Percentage;
                if (result >= total && result < total + value)
                    return odd.Exception;
                total += value;
            }
            return default;
        }
        public async Task<bool> DeleteAsync(TKey key)
        {
            await Task.Delay(GetRandomNumber(_settings.MillisecondsOfWaitForDelete));
            var exception = GetException(_settings.ExceptionOddsForDelete);
            if (exception != null)
            {
                await Task.Delay(GetRandomNumber(_settings.MillisecondsOfWaitBeforeExceptionForDelete));
                throw exception;
            }
            if (Values.ContainsKey(key))
                return Values.Remove(key);
            return false;
        }

        public async Task<T?> GetAsync(TKey key)
        {
            await Task.Delay(GetRandomNumber(_settings.MillisecondsOfWaitForGet));
            var exception = GetException(_settings.ExceptionOddsForGet);
            if (exception != null)
            {
                await Task.Delay(GetRandomNumber(_settings.MillisecondsOfWaitBeforeExceptionForGet));
                throw exception;
            }
            return Values.ContainsKey(key) ? Values[key] : default;
        }

        public async Task<bool> InsertAsync(TKey key, T value)
        {
            await Task.Delay(GetRandomNumber(_settings.MillisecondsOfWaitForInsert));
            var exception = GetException(_settings.ExceptionOddsForInsert);
            if (exception != null)
            {
                await Task.Delay(GetRandomNumber(_settings.MillisecondsOfWaitBeforeExceptionForInsert));
                throw exception;
            }
            if (!Values.ContainsKey(key))
            {
                Values.Add(key, value);
                return true;
            }
            else
                return false;
        }
        public async Task<bool> UpdateAsync(TKey key, T value)
        {
            await Task.Delay(GetRandomNumber(_settings.MillisecondsOfWaitForUpdate));
            var exception = GetException(_settings.ExceptionOddsForUpdate);
            if (exception != null)
            {
                await Task.Delay(GetRandomNumber(_settings.MillisecondsOfWaitBeforeExceptionForUpdate));
                throw exception;
            }
            if (Values.ContainsKey(key))
            {
                Values[key] = value;
                return true;
            }
            else
                return false;
        }

        public async Task<IEnumerable<T>> QueryAsync(Func<T, bool>? predicate = null, int top = 0, int skip = 0)
        {
            await Task.Delay(GetRandomNumber(_settings.MillisecondsOfWaitForQuery));
            var exception = GetException(_settings.ExceptionOddsForQuery);
            if (exception != null)
            {
                await Task.Delay(GetRandomNumber(_settings.MillisecondsOfWaitBeforeExceptionForQuery));
                throw exception;
            }
            if (predicate == null)
                return Values.Select(x => x.Value);
            else
                return Values.Select(x => x.Value).Where(predicate);
        }
    }
}