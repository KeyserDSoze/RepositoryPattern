using Fare;

namespace RepositoryPattern.Data
{
    internal class RegexPopulationService : IPopulationService
    {
        private readonly string[] _regexes;
        public RegexPopulationService(string[] regexes)
        {
            _regexes = regexes;
        }
        public dynamic GetValue(Type type, int numberOfEntities, string treeName)
        {
            var seed = _regexes.First();
            if (!string.IsNullOrWhiteSpace(seed))
            {
                var xeger = new Xeger(seed);
                var generatedString = xeger.Generate();
                if (type.Name.Contains("Nullable`1"))
                    type = type.GenericTypeArguments[0];
                if (type == typeof(Guid))
                    return Guid.Parse(generatedString);
                else if (type == typeof(DateTimeOffset))
                    return DateTimeOffset.Parse(generatedString);
                else if (type == typeof(TimeSpan))
                    return new TimeSpan(long.Parse(generatedString));
                else if (type == typeof(nint))
                    return nint.Parse(generatedString);
                else if (type == typeof(nuint))
                    return nuint.Parse(generatedString);
                else if (type == typeof(Range))
                {
                    var first = int.Parse(generatedString);
                    xeger = new Xeger(_regexes.Last());
                    generatedString = xeger.Generate();
                    var second = int.Parse(generatedString);
                    if (first > second)
                    {
                        var lied = first;
                        first = second;
                        second = lied;
                    }
                    if (first == second)
                        second++;
                    return new Range(new Index(first), new Index(second));
                }
                else
                    return Convert.ChangeType(generatedString, type);
            }
            return null!;
        }
    }
}