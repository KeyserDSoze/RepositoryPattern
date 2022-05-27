using Fare;

namespace RepositoryPattern.Services
{
    internal class RegexService : IRegexService
    {
        public string GetRandomString(string pattern)
        {
            var xeger = new Xeger(pattern);
            var generatedString = xeger.Generate();
            return generatedString;
        }
    }
}