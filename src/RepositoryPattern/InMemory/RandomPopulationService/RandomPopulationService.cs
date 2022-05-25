using Fare;
using RepositoryPattern.Utility;
using System.Collections;
using System.Reflection;
using System.Security.Cryptography;

namespace RepositoryPattern.Data
{
    internal static class RandomPopulationService<T, TKey>
        where TKey : notnull
    {
        private static dynamic GetValue(Type type, string treeName, int index, Func<dynamic> create)
        {
            var dictionary = RepositoryPatternInMemorySettingsFactory.Instance.Settings[Naming.Settings<T, TKey>()].RegexForValueCreation;
            if (dictionary.ContainsKey(treeName))
            {
                var seed = index < dictionary[treeName].Length ?
                    dictionary[treeName][index] : dictionary[treeName].Last();
                if (!string.IsNullOrWhiteSpace(seed))
                {
                    var xeger = new Xeger(seed);
                    var generatedString = xeger.Generate();
                    if (type.Name.Contains("Nullable`1"))
                        type = type.GenericTypeArguments[0];
                    if (type == typeof(Guid))
                        return Guid.Parse(generatedString);
                    else if(type == typeof(DateTimeOffset))
                        return DateTimeOffset.Parse(generatedString);
                    else
                        return Convert.ChangeType(generatedString, type);
                }
            }
            return create();
        }
        public static dynamic? Construct(Type type, string name, int numberOfEntities, string treeName, int forcedIndex = 0)
        {
            if (type == typeof(int) || type == typeof(int?))
            {
                return GetValue(type, treeName, forcedIndex, () => BitConverter.ToInt32(RandomNumberGenerator.GetBytes(4)));
            }
            else if (type == typeof(uint) || type == typeof(uint?))
            {
                return GetValue(type, treeName, forcedIndex, () => BitConverter.ToUInt32(RandomNumberGenerator.GetBytes(4)));
            }
            else if (type == typeof(byte) || type == typeof(byte?))
            {
                return GetValue(type, treeName, forcedIndex, () => RandomNumberGenerator.GetBytes(1)[0]);
            }
            else if (type == typeof(sbyte) || type == typeof(sbyte?))
            {
                return GetValue(type, treeName, forcedIndex, () => (sbyte)RandomNumberGenerator.GetBytes(1)[0]);
            }
            else if (type == typeof(short) || type == typeof(short?))
            {
                return GetValue(type, treeName, forcedIndex, () => BitConverter.ToInt16(RandomNumberGenerator.GetBytes(2)));
            }
            else if (type == typeof(ushort) || type == typeof(ushort?))
            {
                return GetValue(type, treeName, forcedIndex, () => BitConverter.ToUInt16(RandomNumberGenerator.GetBytes(2)));
            }
            else if (type == typeof(long) || type == typeof(long?))
            {
                return GetValue(type, treeName, forcedIndex, () => BitConverter.ToInt64(RandomNumberGenerator.GetBytes(8)));
            }
            else if (type == typeof(ulong) || type == typeof(ulong?))
            {
                return GetValue(type, treeName, forcedIndex, () => BitConverter.ToUInt64(RandomNumberGenerator.GetBytes(8)));
            }
            else if (type == typeof(nint) || type == typeof(nint?))
            {
                return GetValue(type, treeName, forcedIndex, () => (nint)BitConverter.ToInt16(RandomNumberGenerator.GetBytes(2)));
            }
            else if (type == typeof(nuint) || type == typeof(nuint?))
            {
                return GetValue(type, treeName, forcedIndex, () => (nuint)BitConverter.ToUInt16(RandomNumberGenerator.GetBytes(2)));
            }
            else if (type == typeof(float) || type == typeof(float?))
            {
                return GetValue(type, treeName, forcedIndex, () => BitConverter.ToSingle(RandomNumberGenerator.GetBytes(4)));
            }
            else if (type == typeof(double) || type == typeof(double?))
            {
                return GetValue(type, treeName, forcedIndex, () => BitConverter.ToDouble(RandomNumberGenerator.GetBytes(8)));
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                return GetValue(type, treeName, forcedIndex, () => new decimal(BitConverter.ToInt32(RandomNumberGenerator.GetBytes(4)),
                    BitConverter.ToInt32(RandomNumberGenerator.GetBytes(4)),
                    BitConverter.ToInt32(RandomNumberGenerator.GetBytes(4)),
                    RandomNumberGenerator.GetInt32(4) > forcedIndex,
                    (byte)RandomNumberGenerator.GetInt32(29)));
            }
            else if (type == typeof(string))
            {
                return GetValue(type, treeName, forcedIndex, () => $"{name}_{Guid.NewGuid()}");
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                return GetValue(type, treeName, forcedIndex, () => RandomNumberGenerator.GetInt32(4) > 1);
            }
            else if (type == typeof(char) || type == typeof(char?))
            {
                return GetValue(type, treeName, forcedIndex, () => (char)RandomNumberGenerator.GetInt32(256));
            }
            else if (type == typeof(Guid) || type == typeof(Guid?))
            {
                return GetValue(type, treeName, forcedIndex, () => Guid.NewGuid());
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return GetValue(type, treeName, forcedIndex, () => DateTime.UtcNow);
            }
            else if (type == typeof(TimeSpan) || type == typeof(TimeSpan?))
            {
                return TimeSpan.FromTicks(GetValue(typeof(long), treeName, forcedIndex, () => RandomNumberGenerator.GetInt32(200_000)));
            }
            else if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
            {
                return GetValue(type, treeName, forcedIndex, () => DateTimeOffset.UtcNow);
            }
            else if (type == typeof(Range) || type == typeof(Range?))
            {
                int firstNumber = GetValue(typeof(int), treeName, forcedIndex, () => RandomPopulationService<T, TKey>.Construct(typeof(int), "X", forcedIndex, treeName)!);
                int secondNumber = GetValue(typeof(int), treeName, forcedIndex, () => RandomPopulationService<T, TKey>.Construct(typeof(int), "Y", forcedIndex, treeName, 1)!);
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
            else if (!type.IsArray)
            {
                var interfaces = type.GetInterfaces();
                if (type.Name.Contains("IDictionary`2") || interfaces.Any(x => x.Name.Contains("IDictionary`2")))
                {
                    var keyType = type.GetGenericArguments().First();
                    var valueType = type.GetGenericArguments().Last();
                    var dictionaryType = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);
                    var entity = Activator.CreateInstance(dictionaryType)! as IDictionary;
                    for (int i = 0; i < 10; i++)
                    {
                        var newKey = RandomPopulationService<T, TKey>.Construct(type.GetGenericArguments().First(), "Key", forcedIndex, treeName);
                        var newValue = RandomPopulationService<T, TKey>.Construct(type.GetGenericArguments().Last(), "Value", numberOfEntities, treeName, 1);
                        entity!.Add(newKey, newValue);
                    }
                    return entity;
                }
                else if (type.Name.Contains("IEnumerable`1") || interfaces.Any(x => x.Name.Contains("IEnumerable`1")))
                {
                    var valueType = type.GetGenericArguments().First();
                    var listType = typeof(List<>).MakeGenericType(valueType);
                    var entity = Activator.CreateInstance(listType)! as IList;
                    for (int i = 0; i < 10; i++)
                    {
                        var newValue = RandomPopulationService<T, TKey>.Construct(type.GetGenericArguments().First(), string.Empty, numberOfEntities, treeName);
                        entity!.Add(newValue);
                    }
                    return entity;
                }
                else
                {
                    if (!type.IsInterface && !type.IsAbstract)
                    {
                        var entity = Activator.CreateInstance(type);
                        try
                        {
                            var properties = type.GetProperties();
                            foreach (var property in properties)
                            {
                                property.SetValue(entity, RandomPopulationService<T, TKey>.Construct(property, numberOfEntities, treeName));
                            }
                        }
                        catch
                        {
                        }
                        return entity;
                    }
                }
            }
            else if (type.IsArray)
            {
                var entity = Activator.CreateInstance(type, numberOfEntities);
                var valueType = type.GetElementType();
                for (int i = 0; i < numberOfEntities; i++)
                    (entity as dynamic)![i] = RandomPopulationService<T, TKey>.Construct(valueType!, string.Empty, numberOfEntities, treeName);
                return entity;
            }
            return default;
        }
        public static dynamic? Construct(PropertyInfo propertyInfo, int numberOfEntities, string treeName)
        {
            Type type = propertyInfo.PropertyType;
            return Construct(type, propertyInfo.Name, numberOfEntities,
                string.IsNullOrWhiteSpace(treeName) ? propertyInfo.Name : $"{treeName}.{propertyInfo.Name}");
        }
    }
}