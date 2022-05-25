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
            try
            {
                int firstNumber = _populationService.Construct(typeof(int), numberOfEntities, treeName, "X");
                int secondNumber = _populationService.Construct(typeof(int), numberOfEntities, treeName, "Y");
                if (firstNumber < 0)
                    firstNumber *= -1;
                if (secondNumber < 0)
                    secondNumber *= -1;
                if (firstNumber > secondNumber)
                {
                    var lied = firstNumber;
                    firstNumber = secondNumber;
                    secondNumber = lied;
                }
                if (firstNumber == secondNumber)
                    secondNumber++;
                return new Range(new Index(firstNumber), new Index(secondNumber));
            }
            catch (Exception ex)
            {
                string olaf = ex.ToString();
            }
            return null;
        }
    }
}