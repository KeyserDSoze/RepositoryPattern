using System.Security.Cryptography;

namespace RepositoryPattern.Data
{
    internal class NumberPopulationService : IPopulationService
    {
        private readonly int _numberOfBytes;
        private readonly Func<byte[], dynamic> _action;

        public NumberPopulationService(int numberOfBytes, Func<byte[], dynamic> action)
        {
            _numberOfBytes = numberOfBytes;
            _action = action;
        }
        public dynamic GetValue(Type type, int numberOfEntities, string treeName)
            => _action.Invoke(RandomNumberGenerator.GetBytes(_numberOfBytes));
    }
}