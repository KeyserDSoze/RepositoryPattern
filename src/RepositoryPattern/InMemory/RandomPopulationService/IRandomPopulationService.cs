using System.Reflection;

namespace RepositoryPattern.Data
{
    internal interface IRandomPopulationService
    {
        dynamic? Construct(PropertyInfo propertyInfo, int numberOfEntities, string treeName);
        dynamic? Construct(Type type, int numberOfEntities, string treeName, string propertyName);
    }
}