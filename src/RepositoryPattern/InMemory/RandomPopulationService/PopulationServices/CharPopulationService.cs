using System.Security.Cryptography;

namespace RepositoryPattern.Data
{
    internal class CharPopulationService : IPopulationService
    {
        public dynamic GetValue(Type type, int numberOfEntities, string treeName)
            => (char)RandomNumberGenerator.GetInt32(256);
    }
}