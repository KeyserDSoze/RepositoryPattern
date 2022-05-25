using System.Security.Cryptography;

namespace RepositoryPattern.Data
{
    internal class TimePopulationService : IPopulationService
    {
        public dynamic GetValue(Type type, int numberOfEntities, string treeName)
        {
            if (type == typeof(DateTime) || type == typeof(DateTime?))
                return DateTime.UtcNow;
            else if (type == typeof(TimeSpan) || type == typeof(TimeSpan?))
                return TimeSpan.FromTicks(RandomNumberGenerator.GetInt32(200_000));
            else
                return DateTimeOffset.UtcNow;
        }
    }
}