using System.Collections;
using System.Reflection;
using System.Security.Cryptography;

namespace RepositoryPattern.Data
{
    internal static class RandomPopulationService
    {
        public static dynamic? Construct(Type type, string name, int numberOfEntities)
        {
            if (type == typeof(int) || type == typeof(int?))
            {
                return BitConverter.ToInt32(RandomNumberGenerator.GetBytes(4));
            }
            else if (type == typeof(uint) || type == typeof(uint?))
            {
                return BitConverter.ToUInt32(RandomNumberGenerator.GetBytes(4));
            }
            else if (type == typeof(byte) || type == typeof(byte?))
            {
                return RandomNumberGenerator.GetBytes(1)[0];
            }
            else if (type == typeof(sbyte) || type == typeof(sbyte?))
            {
                return (sbyte)RandomNumberGenerator.GetBytes(1)[0];
            }
            else if (type == typeof(short) || type == typeof(short?))
            {
                return BitConverter.ToInt16(RandomNumberGenerator.GetBytes(2));
            }
            else if (type == typeof(ushort) || type == typeof(ushort?))
            {
                return BitConverter.ToUInt16(RandomNumberGenerator.GetBytes(2));
            }
            else if (type == typeof(long) || type == typeof(long?))
            {
                return BitConverter.ToInt64(RandomNumberGenerator.GetBytes(8));
            }
            else if (type == typeof(ulong) || type == typeof(ulong?))
            {
                return BitConverter.ToUInt64(RandomNumberGenerator.GetBytes(8));
            }
            else if (type == typeof(nint) || type == typeof(nint?))
            {
                return (nint)BitConverter.ToInt16(RandomNumberGenerator.GetBytes(2));
            }
            else if (type == typeof(nuint) || type == typeof(nuint?))
            {
                return (nuint)BitConverter.ToUInt16(RandomNumberGenerator.GetBytes(2));
            }
            else if (type == typeof(float) || type == typeof(float?))
            {
                return BitConverter.ToSingle(RandomNumberGenerator.GetBytes(4));
            }
            else if (type == typeof(double) || type == typeof(double?))
            {
                return BitConverter.ToDouble(RandomNumberGenerator.GetBytes(8));
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                return new decimal(BitConverter.ToInt32(RandomNumberGenerator.GetBytes(4)),
                    BitConverter.ToInt32(RandomNumberGenerator.GetBytes(4)),
                    BitConverter.ToInt32(RandomNumberGenerator.GetBytes(4)),
                    RandomNumberGenerator.GetInt32(4) > 1,
                    (byte)RandomNumberGenerator.GetInt32(29));
            }
            else if (type == typeof(string))
            {
                return $"{name}_{Guid.NewGuid()}";
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                return RandomNumberGenerator.GetInt32(4) > 1;
            }
            else if (type == typeof(char) || type == typeof(char?))
            {
                return (char)RandomNumberGenerator.GetInt32(256);
            }
            else if (type == typeof(Guid) || type == typeof(Guid?))
            {
                return Guid.NewGuid();
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return DateTime.UtcNow;
            }
            else if (type == typeof(TimeSpan) || type == typeof(TimeSpan?))
            {
                return TimeSpan.FromTicks(RandomNumberGenerator.GetInt32(200_000));
            }
            else if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
            {
                return DateTimeOffset.UtcNow;
            }
            else if (type == typeof(Range) || type == typeof(Range?))
            {
                return new Range(new Index(RandomNumberGenerator.GetInt32(200_000)), new Index(RandomNumberGenerator.GetInt32(600_000) + 400_000));
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
                        var newKey = RandomPopulationService.Construct(type.GetGenericArguments().First(), "Key", 1);
                        var newValue = RandomPopulationService.Construct(type.GetGenericArguments().Last(), "Value", numberOfEntities);
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
                        var newValue = RandomPopulationService.Construct(type.GetGenericArguments().First(), string.Empty, numberOfEntities);
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
                                property.SetValue(entity, RandomPopulationService.Construct(property, numberOfEntities));
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
                    (entity as dynamic)![i] = RandomPopulationService.Construct(valueType!, string.Empty, numberOfEntities);
                return entity;
            }
            return default;
        }
        public static dynamic? Construct(PropertyInfo propertyInfo, int numberOfEntities)
        {
            Type type = propertyInfo.PropertyType;
            return Construct(type, propertyInfo.Name, numberOfEntities);
        }
    }
}