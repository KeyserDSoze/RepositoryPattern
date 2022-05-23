using System.Collections;
using System.Reflection;
using System.Security.Cryptography;

namespace RepositoryPattern.Data
{
    internal static class Creator
    {
        public static dynamic Construct(Type type, string name, int numberOfEntities)
        {
            if (type == typeof(int) || type == typeof(int?))
            {
                return new Random().Next(int.MinValue, int.MaxValue);
            }
            else if (type == typeof(long) || type == typeof(long?))
            {
                return new Random().NextInt64(long.MinValue, long.MaxValue);
            }
            else if (type == typeof(uint) || type == typeof(uint?))
            {
                return new Random().NextInt64(uint.MinValue, uint.MaxValue);
            }
            else if (type == typeof(ulong) || type == typeof(ulong?))
            {
                return new Random().NextInt64(0, long.MaxValue);
            }
            else if (type == typeof(short) || type == typeof(short?))
            {
                return new Random().NextInt64(short.MinValue, short.MaxValue);
            }
            else if (type == typeof(ushort) || type == typeof(ushort?))
            {
                return new Random().NextInt64(ushort.MinValue, ushort.MaxValue);
            }
            else if (type == typeof(float) || type == typeof(float?))
            {
                return (float)(new Random().NextDouble() * float.MaxValue + float.MinValue);
            }
            else if (type == typeof(double) || type == typeof(double?))
            {
                return new Random().NextDouble() * double.MaxValue + double.MinValue;
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                return (decimal)new Random().NextDouble() * decimal.MaxValue + decimal.MinValue;
            }
            else if (type == typeof(string))
            {
                return $"{name}_{Guid.NewGuid()}";
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                return new Random().NextDouble() > 0.5D;
            }
            else if (type == typeof(char) || type == typeof(char?))
            {
                return (char)new Random().Next(0, 255);
            }
            else if (type == typeof(Guid) || type == typeof(Guid?))
            {
                return Guid.NewGuid();
            }
            else if (type == typeof(byte) || type == typeof(byte?))
            {
                return RandomNumberGenerator.GetBytes(1)[0];
            }
            else if (type == typeof(sbyte) || type == typeof(sbyte?))
            {
                return (sbyte)new Random().Next(0, 255);
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return DateTime.UtcNow;
            }
            else if (type == typeof(TimeSpan) || type == typeof(TimeSpan?))
            {
                return TimeSpan.FromTicks(new Random().Next(int.MaxValue));
            }
            else if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
            {
                return DateTimeOffset.UtcNow;
            }
            else if (type == typeof(Range) || type == typeof(Range?))
            {
                return new Range(new Index(new Random().Next(0, 100_000)), new Index(new Random().Next(200_000, 800_000)));
            }
            else if (!type.IsArray && !type.IsInterface && !type.IsAbstract)
            {
                var interfaces = type.GetInterfaces();
                if (interfaces.Any(x => x.Name.Contains("IDictionary`2")))
                {
                    var keyType = type.GetGenericArguments().First();
                    var valueType = type.GetGenericArguments().Last();
                    var dictionaryType = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);
                    var entity = Activator.CreateInstance(dictionaryType)! as IDictionary;
                    for (int i = 0; i < 10; i++)
                    {
                        var newKey = Creator.Construct(type.GetGenericArguments().First(), "Key", 1);
                        var newValue = Creator.Construct(type.GetGenericArguments().Last(), "Value", numberOfEntities);
                        entity!.Add(newKey, newValue);
                    }
                }
                else if (interfaces.Any(x => x.Name.Contains("IEnumerable`1")))
                {
                    var valueType = type.GetGenericArguments().First();
                    var listType = typeof(List<>).MakeGenericType(valueType);
                    var entity = Activator.CreateInstance(listType)! as IList;
                    for (int i = 0; i < 10; i++)
                    {
                        var newValue = Creator.Construct(type.GetGenericArguments().First(), string.Empty, numberOfEntities);
                        entity!.Add(newValue);
                    }
                }
                else
                {
                    var entity = Activator.CreateInstance(type);
                    try
                    {
                        var properties = type.GetProperties();
                        foreach (var property in properties)
                        {
                            property.SetValue(entity, Creator.Construct(property, numberOfEntities));
                        }
                    }
                    catch
                    {
                    }
                    return entity;
                }
            }
            else if (type.IsArray)
            {
                var entity = Activator.CreateInstance(type, numberOfEntities);
                var valueType = type.GetElementType();
                for (int i = 0; i < numberOfEntities; i++)
                {
                    ((dynamic)entity)[i] = Creator.Construct(valueType, string.Empty, numberOfEntities);
                }
            }
            return default;
        }
        public static dynamic Construct(PropertyInfo propertyInfo, int numberOfEntities)
        {
            Type type = propertyInfo.PropertyType;
            return Construct(type, propertyInfo.Name, numberOfEntities);
        }
    }
}