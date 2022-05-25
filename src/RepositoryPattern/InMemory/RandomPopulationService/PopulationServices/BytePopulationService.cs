using System.Security.Cryptography;

namespace RepositoryPattern.Data
{
    internal class BytePopulationService : IPopulationService
    {
        public dynamic GetValue(Type type, int numberOfEntities, string treeName)
        {
            if (type == typeof(byte) || type == typeof(byte?))
                return RandomNumberGenerator.GetBytes(1)[0];
            else
                return (sbyte)RandomNumberGenerator.GetBytes(1)[0];
        }
    }
}