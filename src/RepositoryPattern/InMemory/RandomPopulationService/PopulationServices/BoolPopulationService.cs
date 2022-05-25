using System.Security.Cryptography;

namespace RepositoryPattern.Data
{
    internal class BoolPopulationService : IPopulationService
    {
        public dynamic GetValue(Type type, int numberOfEntities, string treeName)
            => RandomNumberGenerator.GetInt32(4) > 1;
    }
}