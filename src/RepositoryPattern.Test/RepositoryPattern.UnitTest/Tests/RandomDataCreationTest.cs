using RepositoryPattern.UnitTest.Models;
using System.Threading.Tasks;
using Xunit;

namespace RepositoryPattern.UnitTest
{
    public class RandomDataCreationTest
    {
        private readonly IRepositoryPattern<User, string> _user;
        public RandomDataCreationTest(IRepositoryPattern<User, string> user)
        {
            _user = user;
        }
        [Fact]
        public async Task TestAsync()
        {
            var users = await _user.ToListAsync();
            Assert.True(users.Count == 100);
        }
    }
}