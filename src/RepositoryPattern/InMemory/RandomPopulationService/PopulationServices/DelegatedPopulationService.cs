namespace RepositoryPattern.Data
{
    internal class DelegatedPopulationService : IPopulationService
    {
        private readonly Func<dynamic> _action;
        public DelegatedPopulationService(Func<dynamic> action)
        {
            _action = action;
        }
        public dynamic GetValue(Type type, int numberOfEntities, string treeName)
            => _action.Invoke();
    }
}