// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using RepositoryPattern.Client;
using RepositoryPatternApp;
using Rystem;

//await new UserRepository().QueryAsync(x => x.Id == "A" && x.Email == "B");
//string pattern = @"(?:2018|2019|2020|2021|2022)/(?:10|11|12)/(?:06|07|08) (00:00:00)";
//string pattern = @"[a-z]{4,10}@gmail\.com";
//var xeger = new Xeger(pattern);
//var generatedString = xeger.Generate();
//Console.WriteLine(generatedString);

ServiceLocator
    .Create()
    .AddRepositoryPatternClient<User, string>("localhost:7058");
//    .AddRepositoryPatternInMemoryStorage<User, string>()
//    .PopulateWithRandomData(x => x.Id!)
//    .WithPattern(x => x.Email, "[a-z]{4,5}")
//    .And()
//    .AddRepositoryPatternInMemoryStorage<Solomon, string>(options =>
//    {
//        var customRange = new Range(1000, 2000);
//        options.MillisecondsOfWaitForDelete = customRange;
//        options.MillisecondsOfWaitForInsert = customRange;
//        options.MillisecondsOfWaitForUpdate = customRange;
//        options.MillisecondsOfWaitForGet = customRange;
//        options.MillisecondsOfWaitForQuery = new Range(3000, 7000);
//        options.MillisecondsOfWaitBeforeExceptionForDelete = customRange;
//        options.MillisecondsOfWaitBeforeExceptionForInsert = customRange;
//        options.MillisecondsOfWaitBeforeExceptionForUpdate = customRange;
//        options.MillisecondsOfWaitBeforeExceptionForGet = customRange;
//        options.MillisecondsOfWaitBeforeExceptionForQuery = new Range(3000, 7000);
//        var customExceptions = new List<ExceptionOdds>
//        {
//            new ExceptionOdds()
//            {
//                Exception = new Exception(),
//                Percentage = 0.45
//            },
//            new ExceptionOdds()
//            {
//                Exception = new Exception("Big Exception"),
//                Percentage = 0.1
//            },
//            new ExceptionOdds()
//            {
//                Exception = new Exception("Great Exception"),
//                Percentage = 0.548
//            }
//        };
//        options.ExceptionOddsForDelete.AddRange(customExceptions);
//        options.ExceptionOddsForGet.AddRange(customExceptions);
//        options.ExceptionOddsForInsert.AddRange(customExceptions);
//        options.ExceptionOddsForUpdate.AddRange(customExceptions);
//        options.ExceptionOddsForQuery.AddRange(customExceptions);
//    })
//    .PopulateWithRandomData(x => x.Key!, 20)
//    .WithPattern(x => x.Key, "[a-z]{4,16}")
//    .WithPattern(x => x.Casualty!.Folder, "[a-z]{1,4}")
//    .WithPattern(x => x.Headers, "", "[a-z]{3,4}")
//    .WithPattern(x => x.Olaf, "[1-9]{3,4}")
//    .WithValue(x => x.Z, () => new Range(new Index(1), new Index(2)))
//    .And()
//    .Finalize()
//    .FinalizeWithoutDependencyInjection();
//ServiceLocator.GetService<IServiceProvider>()
//    .Populate();

//var storage = ServiceLocator.GetService<IRepositoryPattern<Solomon, string>>();
var storage = ServiceLocator.GetService<IRepositoryPatternClient<User, string>>();
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable IDE0059 // Unnecessary assignment of a value
var all = await storage.QueryAsync();
#pragma warning restore IDE0079 // Remove unnecessary suppression
await storage.InsertAsync("aaa", new("aaa") { Id = "a", Name = "b" });
await storage.UpdateAsync("aaa", new("aaa") { Id = "a3", Name = "b3" });
var q = await storage.GetAsync("aaa");
await storage.DeleteAsync("aaa");
all = await storage.QueryAsync();
var olaf = string.Empty;
#pragma warning restore IDE0059 // Unnecessary assignment of a value