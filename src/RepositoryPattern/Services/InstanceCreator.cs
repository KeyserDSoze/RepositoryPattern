using RepositoryPattern.Population;

namespace RepositoryPattern.Services
{
    public class InstanceCreator : IInstanceCreator
    {
        public object? CreateInstance(Type type, IPopulationService populationService, int numberOfEntities, string treeName)
        {
            var constructor = type.GetConstructors().OrderBy(x => x.GetParameters().Length).FirstOrDefault();
            if (constructor == null)
                return null;
            else if (constructor.GetParameters().Length == 0)
                return Activator.CreateInstance(type);
            else
            {
                List<object> instances = new();
                foreach (var x in constructor.GetParameters())
                    instances.Add(populationService.Construct(x.ParameterType, numberOfEntities, treeName, $"{x.Name![0..1].ToUpper()}{x.Name[1..]}"));
                return Activator.CreateInstance(type, instances.ToArray());
            }
        }
    }
}