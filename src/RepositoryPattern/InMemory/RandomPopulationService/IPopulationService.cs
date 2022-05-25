namespace RepositoryPattern.Data
{
    internal interface IPopulationService
    {
        dynamic GetValue(Type type, int numberOfEntities, string treeName);
    }
}