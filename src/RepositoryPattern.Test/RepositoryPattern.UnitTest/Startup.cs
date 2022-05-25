using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryPattern.UnitTest.Models;
using Rystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryPattern.UnitTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
                .AddEnvironmentVariables()
                .Build();
            services.AddSingleton(configuration);
            services
                .AddRystem()
                .AddRepositoryPatternInMemoryStorage<User, string>(options =>
                {
                    var writingRange = new Range(int.Parse(configuration["data_creation:delay_in_write_from"]),
                        int.Parse(configuration["data_creation:delay_in_write_to"]));
                    options.MillisecondsOfWaitForDelete = writingRange;
                    options.MillisecondsOfWaitForInsert = writingRange;
                    options.MillisecondsOfWaitForUpdate = writingRange;
                    var readingRange = new Range(int.Parse(configuration["data_creation:delay_in_read_from"]),
                        int.Parse(configuration["data_creation:delay_in_read_to"]));
                    options.MillisecondsOfWaitForGet = readingRange;
                    options.MillisecondsOfWaitForWhere = readingRange;
                })
                .PopulateWithRandomData()
                .Populate(x => x.Id!, 100)
                .AddRepositoryPatternInMemoryStorage<Car, string>(options =>
                {
                    var customExceptions = new List<ExceptionOdds>
                    {
                        new ExceptionOdds()
                        {
                            Exception = new Exception("Normal Exception"),
                            Percentage = 10.352
                        },
                        new ExceptionOdds()
                        {
                            Exception = new Exception("Big Exception"),
                            Percentage = 49.1
                        },
                        new ExceptionOdds()
                        {
                            Exception = new Exception("Great Exception"),
                            Percentage = 40.548
                        }
                    };
                    options.ExceptionOddsForDelete.AddRange(customExceptions);
                    options.ExceptionOddsForGet.AddRange(customExceptions);
                    options.ExceptionOddsForInsert.AddRange(customExceptions);
                    options.ExceptionOddsForUpdate.AddRange(customExceptions);
                    options.ExceptionOddsForWhere.AddRange(customExceptions);
                })
                .AddRepositoryPatternInMemoryStorageWithStringKey<PopulationTest>()
                .PopulateWithRandomData()
                .WithPattern(x => x.J!.First().A, "[a-z]{4,5}")
                .WithPattern(x => x.Y!.First().Value.A, "[a-z]{4,5}")
                .Populate(x => x.P)
                .AddRepositoryPatternInMemoryStorageWithStringKey<RegexPopulationTest>()
                .PopulateWithRandomData()
                .WithPattern(x => x.A, "[1-9]{1,2}")
                .WithPattern(x => x.AA, "[1-9]{1,2}")
                .WithPattern(x => x.B, "[1-9]{1,2}")
                .WithPattern(x => x.BB, "[1-9]{1,2}")
                .WithPattern(x => x.C, "[1-9]{1,2}")
                .WithPattern(x => x.CC, "[1-9]{1,2}")
                .WithPattern(x => x.D, "[1-9]{1,2}")
                .WithPattern(x => x.DD, "[1-9]{1,2}")
                .WithPattern(x => x.E, "[1-9]{1,2}")
                .WithPattern(x => x.EE, "[1-9]{1,2}")
                .WithPattern(x => x.F, "[1-9]{1,2}")
                .WithPattern(x => x.FF, "[1-9]{1,2}")
                .WithPattern(x => x.G, "[1-9]{1,2}")
                .WithPattern(x => x.GG, "[1-9]{1,2}")
                .WithPattern(x => x.H, "")
                .WithPattern(x => x.HH, string.Empty)
                .WithPattern(x => x.L, string.Empty)
                .WithPattern(x => x.LL, "    ")
                .WithPattern(x => x.M, "[1-9]{1,2}")
                .WithPattern(x => x.MM, "[1-9]{1,2}")
                .WithPattern(x => x.N, "[1-9]{1,2}")
                .WithPattern(x => x.NN, "[1-9]{1,2}")
                .WithPattern(x => x.O, "[1-9]{1,2}")
                .WithPattern(x => x.OO, "[1-9]{1,2}")
                .WithPattern(x => x.P, "[1-9]{1,2}")
                .WithPattern(x => x.Q, "true")
                .WithPattern(x => x.QQ, "true")
                .WithPattern(x => x.R, "[a-z]{1}")
                .WithPattern(x => x.RR, "[a-z]{1}")
                .WithPattern(x => x.S, "([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})")
                .WithPattern(x => x.SS, "([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})")
                .WithPattern(x => x.T, @"(?:2018|2019|2020|2021|2022)/(?:10|11|12)/(?:06|07|08) (00:00:00)")
                .WithPattern(x => x.TT, @"(?:2018|2019|2020|2021|2022)/(?:10|11|12)/(?:06|07|08) (00:00:00)")
                .WithPattern(x => x.U, "[1-9]{1,4}")
                .WithPattern(x => x.UU, "[1-9]{1,4}")
                .WithPattern(x => x.V, @"(?:10|11|12)/(?:06|07|08)/(?:2018|2019|2020|2021|2022) (00:00:00 AM \+00:00)")
                .WithPattern(x => x.VV, @"(?:10|11|12)/(?:06|07|08)/(?:2018|2019|2020|2021|2022) (00:00:00 AM \+00:00)")
                .WithPattern(x => x.Z, "[1-9]{1,2}", "[1-9]{1,2}")
                .WithPattern(x => x.ZZ, "[1-9]{1,2}", "[1-9]{1,2}")
                .Populate(x => x.P)
                .Finalize()
                .FinalizeWithoutDependencyInjection();
        }
    }
}
