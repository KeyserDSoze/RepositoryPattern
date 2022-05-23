// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using RepositoryPattern;
using RepositoryPatternApp;
using Rystem;

ServiceLocator
    .Create()
    .AddRepositoryPatternInMemoryStorage<Solomon, string>(options =>
    {
        var customRange = new Range(1000, 2000);
        options.MillisecondsOfWaitForDelete = customRange;
        options.MillisecondsOfWaitForInsert = customRange;
        options.MillisecondsOfWaitForUpdate = customRange;
        options.MillisecondsOfWaitForGet = customRange;
        options.MillisecondsOfWaitForWhere = new Range(3000, 7000);
        var customExceptions = new List<ExceptionOdds>
        {
            new ExceptionOdds()
            {
                Exception = new Exception(),
                Percentage = 0.45
            },
            new ExceptionOdds()
            {
                Exception = new Exception("Big Exception"),
                Percentage = 0.1
            },
            new ExceptionOdds()
            {
                Exception = new Exception("Great Exception"),
                Percentage = 0.548
            }
        };
        options.ExceptionOddsForDelete.AddRange(customExceptions);
        options.ExceptionOddsForGet.AddRange(customExceptions);
        options.ExceptionOddsForInsert.AddRange(customExceptions);
        options.ExceptionOddsForUpdate.AddRange(customExceptions);
        options.ExceptionOddsForWhere.AddRange(customExceptions);
    })
    .PopulateWithRandomData(x => x.Key, 20)
    .Finalize()
    .FinalizeWithoutDependencyInjection();


var storage = ServiceLocator.GetService<IRepositoryPattern<Solomon, string>>();
await storage.InsertAsync("aaa", new());
await storage.UpdateAsync("aaa", new());
var q = await storage.GetAsync("aaa");
await storage.DeleteAsync("aaa");
var all = await storage.ToListAsync();
var olaf = string.Empty;