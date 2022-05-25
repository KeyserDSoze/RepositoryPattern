using System.Security.Cryptography;

namespace RepositoryPattern.Data
{
    internal class DecimalPopulationService : IPopulationService
    {
        public dynamic GetValue(Type type, int numberOfEntities, string treeName)
            => new decimal(BitConverter.ToInt32(RandomNumberGenerator.GetBytes(4)),
                    BitConverter.ToInt32(RandomNumberGenerator.GetBytes(4)),
                    BitConverter.ToInt32(RandomNumberGenerator.GetBytes(4)),
                    RandomNumberGenerator.GetInt32(4) > 1,
                    (byte)RandomNumberGenerator.GetInt32(29));
    }
}