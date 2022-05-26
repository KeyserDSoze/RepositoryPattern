namespace RepositoryPattern.Data
{
    internal class RangePopulationService : IPopulationService
    {
        private readonly IRandomPopulationService _populationService;

        public RangePopulationService(IRandomPopulationService populationService)
        {
            _populationService = populationService;
        }
        public dynamic GetValue(Type type, int numberOfEntities, string treeName)
        {

            int firstNumber = _populationService.Construct(typeof(int), numberOfEntities, treeName, "X");
            int secondNumber = _populationService.Construct(typeof(int), numberOfEntities, treeName, "Y");
            if (firstNumber < 0)
                firstNumber *= -1;
            if (secondNumber < 0)
                secondNumber *= -1;
            if (firstNumber > secondNumber)
                (secondNumber, firstNumber) = (firstNumber, secondNumber);
            if (firstNumber == secondNumber)
                secondNumber++;
            return new Range(new Index(firstNumber), new Index(secondNumber));
        }
    }
}