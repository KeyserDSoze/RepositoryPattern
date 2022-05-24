using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryPattern;
using RepositoryPattern.UnitTest.Models;
using Rystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

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
                .PopulateWithRandomData(x => x.Id!, 100)
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
                .PopulateWithRandomData(x => x.P)
                .Finalize()
                .FinalizeWithoutDependencyInjection();
        }
    }
}
