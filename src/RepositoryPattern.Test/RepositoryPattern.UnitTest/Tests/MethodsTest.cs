using RepositoryPattern.UnitTest.Models;
using System.Threading.Tasks;
using Xunit;

namespace RepositoryPattern.UnitTest
{
    public class MethodsTest
    {
        private readonly IRepositoryPattern<User, string> _user;
        public MethodsTest(IRepositoryPattern<User, string> user)
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