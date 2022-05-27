using RepositoryPattern.Population;

namespace RepositoryPattern.Services
{
    public interface IInstanceCreator
    {
        object? CreateInstance(Type type, IPopulationService populationService, int numberOfEntities, string treeName);
    }
}