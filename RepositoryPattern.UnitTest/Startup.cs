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
                .Finalize()
                .FinalizeWithoutDependencyInjection();
        }
    }
}
