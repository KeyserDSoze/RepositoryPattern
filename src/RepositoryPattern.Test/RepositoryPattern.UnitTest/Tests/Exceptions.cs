using RepositoryPattern.UnitTest.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RepositoryPattern.UnitTest
{
    public class Exceptions
    {
        private readonly IRepositoryPattern<Car, string> _car;
        public Exceptions(IRepositoryPattern<Car, string> car)
        {
            _car = car;
        }
        [Fact]
        public async Task TestAsync()
        {
            try
            {
                var cars = await _car.QueryAsync();
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.True(ex.Message == "Normal Exception" || ex.Message == "Big Exception" || ex.Message == "Great Exception");
            }
        }
    }
}