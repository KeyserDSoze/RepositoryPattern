using RepositoryPattern.Utility;

namespace RepositoryPattern.Data
{
    internal class PopulationServiceFactory<T, TKey>
        where TKey : notnull
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "It needed for DI")]
        public IPopulationService GetService(Type type, IRandomPopulationService populationService, string treeName)
        {
            var delegatedDictionary = RepositoryPatternInMemorySettingsFactory.Instance.Settings[Naming.Settings<T, TKey>()].DelegatedMethodForValueCreation;
            if (delegatedDictionary.ContainsKey(treeName))
                return new DelegatedPopulationService(delegatedDictionary[treeName]);
            var regexDictionary = RepositoryPatternInMemorySettingsFactory.Instance.Settings[Naming.Settings<T, TKey>()].RegexForValueCreation;
            if (regexDictionary.ContainsKey(treeName))
                return new RegexPopulationService(regexDictionary[treeName]);
            var implementationDictionary = RepositoryPatternInMemorySettingsFactory.Instance.Settings[Naming.Settings<T, TKey>()].ImplementationForValueCreation;
            if (implementationDictionary.ContainsKey(treeName))
                return new ImplementationPopulationService(implementationDictionary[treeName], populationService);
            if (type == typeof(int) || type == typeof(int?))
                return new NumberPopulationService(4, (x) => BitConverter.ToInt32(x));
            else if (type == typeof(uint) || type == typeof(uint?))
                return new NumberPopulationService(4, (x) => BitConverter.ToUInt32(x));
            else if (type == typeof(byte) || type == typeof(byte?) || type == typeof(sbyte) || type == typeof(sbyte?))
                return new BytePopulationService();
            else if (type == typeof(short) || type == typeof(short?))
                return new NumberPopulationService(2, (x) => BitConverter.ToInt16(x));
            else if (type == typeof(ushort) || type == typeof(ushort?))
                return new NumberPopulationService(2, (x) => BitConverter.ToUInt16(x));
            else if (type == typeof(long) || type == typeof(long?))
                return new NumberPopulationService(8, (x) => BitConverter.ToInt64(x));
            else if (type == typeof(ulong) || type == typeof(ulong?))
                return new NumberPopulationService(8, (x) => BitConverter.ToUInt64(x));
            else if (type == typeof(nint) || type == typeof(nint?))
                return new NumberPopulationService(2, (x) => (nint)BitConverter.ToInt16(x));
            else if (type == typeof(nuint) || type == typeof(nuint?))
                return new NumberPopulationService(2, (x) => (nuint)BitConverter.ToUInt16(x));
            else if (type == typeof(float) || type == typeof(float?))
                return new NumberPopulationService(4, (x) => BitConverter.ToSingle(x));
            else if (type == typeof(double) || type == typeof(double?))
                return new NumberPopulationService(8, (x) => BitConverter.ToDouble(x));
            else if (type == typeof(decimal) || type == typeof(decimal?))
                return new DecimalPopulationService();
            else if (type == typeof(bool) || type == typeof(bool?))
                return new BoolPopulationService();
            else if (type == typeof(char) || type == typeof(char?))
                return new CharPopulationService();
            else if (type == typeof(Guid) || type == typeof(Guid?))
                return new GuidPopulationService();
            else if (type == typeof(DateTime) || type == typeof(DateTime?) || type == typeof(TimeSpan) || type == typeof(TimeSpan?) || type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
                return new TimePopulationService();
            else if (type == typeof(Range) || type == typeof(Range?))
                return new RangePopulationService(populationService);
            else if (type == typeof(string))
                return new StringPopulationService();
            else if (!type.IsArray)
            {
                var interfaces = type.GetInterfaces();
                if (type.Name.Contains("IDictionary`2") || interfaces.Any(x => x.Name.Contains("IDictionary`2")))
                    return new DictionaryPopulationService(populationService);
                else if (type.Name.Contains("IEnumerable`1") || interfaces.Any(x => x.Name.Contains("IEnumerable`1")))
                    return new EnumerablePopulationService(populationService);
                else
                    return new ObjectPopulationService(populationService);
            }
            else if (type.IsArray)
                return new ArrayPopulationService(populationService);
            else
                return new AbstractPopulationService(populationService);
        }
    }
}