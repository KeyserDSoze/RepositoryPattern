// See https://aka.ms/new-console-template for more information
using Fare;
using Microsoft.Extensions.DependencyInjection;
using RepositoryPattern;
using RepositoryPatternApp;
using Rystem;
using System.Text.RegularExpressions;

string pattern = @"^([a-z]{4,16})@([a-z]{4,5})\.([a-z]{2,3})";
var xeger = new Xeger(pattern);
var generatedString = xeger.Generate();
Console.WriteLine(generatedString);

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
    .PopulateWithRandomData()
    .WithPattern(x => x.Key, "[a-z]{1,4}")
    .WithPattern(x => x.Casualty!.Folder, "[a-z]{1,4}")
    .WithPattern(x => x.Headers, "", "[a-z]{3,4}")
    .WithPattern(x => x.Olaf, "[1-9]{3,4}")
    .Populate(x => x.Key!, 20)
    .Finalize()
    .FinalizeWithoutDependencyInjection();


var storage = ServiceLocator.GetService<IRepositoryPattern<Solomon, string>>();
await storage.InsertAsync("aaa", new());
await storage.UpdateAsync("aaa", new());
var q = await storage.GetAsync("aaa");
await storage.DeleteAsync("aaa");
var all = await storage.QueryAsync();
var olaf = string.Empty;