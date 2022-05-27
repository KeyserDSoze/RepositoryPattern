using RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternApp
{
    public class User
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; }
        public User(string email)
        {
            Email = email;
        }
    }
    public class UserWriter : ICommandPattern<User, string>
    {
        public Task<bool> DeleteAsync(string key)
        {
            //delete on with DB or storage context
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(string key, User value)
        {
            //insert on DB or storage context
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(string key, User value)
        {
            //update on DB or storage context
            throw new NotImplementedException();
        }
    }
    public class UserReader : IQueryPattern<User, string>
    {
        public Task<User?> GetAsync(string key)
        {
            //get an item by key from DB or storage context
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> QueryAsync(Expression<Func<User, bool>>? predicate = null, int? top = null, int? skip = null)
        {
            //get a list of items by a predicate with top and skip from DB or storage context
            throw new NotImplementedException();
        }
    }
    public class UserRepository : IRepositoryPattern<User, string>, IQueryPattern<User, string>, ICommandPattern<User, string>
    {
        public Task<bool> DeleteAsync(string key)
        {
            //delete on with DB or storage context
            throw new NotImplementedException();
        }
        public Task<bool> InsertAsync(string key, User value)
        {
            //insert on DB or storage context
            throw new NotImplementedException();
        }
        public Task<bool> UpdateAsync(string key, User value)
        {
            //update on DB or storage context
            throw new NotImplementedException();
        }
        public Task<User?> GetAsync(string key)
        {
            //get an item by key from DB or storage context
            throw new NotImplementedException();
        }
        public Task<IEnumerable<User>> QueryAsync(Expression<Func<User, bool>>? predicate = null, int? top = null, int? skip = null)
        {
#pragma warning disable IDE0059 // Unnecessary assignment of a value
            var x = predicate!.ToString();
#pragma warning restore IDE0059 // Unnecessary assignment of a value
            //get a list of items by a predicate with top and skip from DB or storage context
            throw new NotImplementedException();
        }
    }

    internal class Solomon
    {
        public Range Z { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }
        public string? Folder { get; set; }
        public int Olaf { get; set; }
        public Casualty? Casualty { get; set; }
        public List<int>? Hellos { get; set; }
        public Dictionary<string, string>? Headers { get; set; }
        public int[]? Consolated { get; set; }
    }
    internal class Casualty
    {
        public string? Value { get; set; }
        public string? Folder { get; set; }
    }
}
