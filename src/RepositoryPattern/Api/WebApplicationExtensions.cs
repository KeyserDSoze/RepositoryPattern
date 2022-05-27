using Microsoft.AspNetCore.Builder;
using RepositoryPattern;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WebApplicationExtensions
    {
        internal static Dictionary<Type, RepositoryPatternService> Services = new();
        public static WebApplication AddApiForRepositoryPattern(this WebApplication app, string startingPath = "api")
        {
            foreach (var service in Services)
            {
                if (service.Value.QueryType != null || service.Value.RepositoryType != null)
                {
                    _ = typeof(WebApplicationExtensions).GetMethod(nameof(AddGet), BindingFlags.NonPublic | BindingFlags.Static)!
                        .MakeGenericMethod(service.Key, service.Value.KeyType, (service.Value.QueryType ?? service.Value.RepositoryType)!)
                        .Invoke(null, new object[3] { app, service.Key.Name, startingPath });
                }
                if (service.Value.QueryType != null || service.Value.RepositoryType != null)
                {
                    _ = typeof(WebApplicationExtensions).GetMethod(nameof(AddQuery), BindingFlags.NonPublic | BindingFlags.Static)!
                        .MakeGenericMethod(service.Key, service.Value.KeyType, (service.Value.QueryType ?? service.Value.RepositoryType)!)
                        .Invoke(null, new object[3] { app, service.Key.Name, startingPath });
                }
                if (service.Value.CommandType != null || service.Value.RepositoryType != null)
                {
                    _ = typeof(WebApplicationExtensions).GetMethod(nameof(AddInsert), BindingFlags.NonPublic | BindingFlags.Static)!
                        .MakeGenericMethod(service.Key, service.Value.KeyType, (service.Value.CommandType ?? service.Value.RepositoryType)!)
                        .Invoke(null, new object[3] { app, service.Key.Name, startingPath });
                }
                if (service.Value.CommandType != null || service.Value.RepositoryType != null)
                {
                    _ = typeof(WebApplicationExtensions).GetMethod(nameof(AddUpdate), BindingFlags.NonPublic | BindingFlags.Static)!
                        .MakeGenericMethod(service.Key, service.Value.KeyType, (service.Value.CommandType ?? service.Value.RepositoryType)!)
                        .Invoke(null, new object[3] { app, service.Key.Name, startingPath });
                }
                if (service.Value.CommandType != null || service.Value.RepositoryType != null)
                {
                    _ = typeof(WebApplicationExtensions).GetMethod(nameof(AddDelete), BindingFlags.NonPublic | BindingFlags.Static)!
                        .MakeGenericMethod(service.Key, service.Value.KeyType, (service.Value.CommandType ?? service.Value.RepositoryType)!)
                        .Invoke(null, new object[3] { app, service.Key.Name, startingPath });
                }
            }
            return app;
        }
        private static void AddGet<T, TKey, TPattern>(WebApplication app, string name, string startingPath)
            where TKey : notnull
        {
            _ = app.MapGet($"{startingPath}/{name.ToLower()}", async (TKey key, TPattern pattern) =>
            {
                var queryPattern = pattern as IQueryPattern<T, TKey>;
                return await queryPattern!.GetAsync(key);
            }).WithName($"Get{name}");
        }
        private static void AddQuery<T, TKey, TPattern>(WebApplication app, string name, string startingPath)
           where TKey : notnull
        {
            _ = app.MapGet($"{startingPath}/{name.ToLower()}/list", async (string? query, int? top, int? skip, TPattern pattern) =>
              {
                  dynamic? expression = null;
                  if (!string.IsNullOrWhiteSpace(query))
                  {
                      var parameter = Expression.Parameter(typeof(T), query.Split(' ').First());
                      expression = DynamicExpressionParser.ParseLambda<T, bool>(ParsingConfig.Default, false, query);
                  }
                  var queryPattern = pattern as IQueryPattern<T, TKey>;
                  return await queryPattern!.QueryAsync(expression, top, skip);
              }).WithName($"List{name}");
        }
        private static void AddInsert<T, TKey, TPattern>(WebApplication app, string name, string startingPath)
          where TKey : notnull
        {
            _ = app.MapPost($"{startingPath}/{name.ToLower()}/insert", async (TKey key, T entity, TPattern pattern) =>
            {
                var commandPattern = pattern as ICommandPattern<T, TKey>;
                return await commandPattern!.InsertAsync(key, entity);
            }).WithName($"Insert{name}");
        }
        private static void AddUpdate<T, TKey, TPattern>(WebApplication app, string name, string startingPath)
          where TKey : notnull
        {
            _ = app.MapPost($"{startingPath}/{name.ToLower()}/update", async (TKey key, T entity, TPattern pattern) =>
            {
                var commandPattern = pattern as ICommandPattern<T, TKey>;
                return await commandPattern!.UpdateAsync(key, entity);
            }).WithName($"Update{name}");
        }
        private static void AddDelete<T, TKey, TPattern>(WebApplication app, string name, string startingPath)
          where TKey : notnull
        {
            _ = app.MapPost($"{startingPath}/{name.ToLower()}/delete", async (TKey key, TPattern pattern) =>
            {
                var commandPattern = pattern as ICommandPattern<T, TKey>;
                return await commandPattern!.DeleteAsync(key);
            }).WithName($"Delete{name}");
        }
    }
}
