using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManager.Models;
using InventoryManager.Services;

namespace InventoryManager.Services
{
    public class UserMockDataStore : IUserDataStore<User>
{
    List<User> users;

    public UserMockDataStore()
    {

    }

    public async Task<bool> AddUserAsync(User user)
    {
        users.Add(user);

        return await Task.FromResult(true);
    }

        public Task<bool> AuthenticateUserAsync(AuthenticationModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteUserAsync(int id)
    {
        var oldUser = users.Where((User arg) => arg.Id == id).FirstOrDefault();
        users.Remove(oldUser);

        return await Task.FromResult(true);
    }

    public async Task<User> GetUserAsync(int id)
    {
        return await Task.FromResult(users.FirstOrDefault(s => s.Id == id));
    }

    public async Task<IEnumerable<User>> GetUsersAsync(bool forceRefresh = false)
    {
        return await Task.FromResult(users);
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        var oldUser = users.Where((User arg) => arg.UserName == user.UserName).FirstOrDefault();
        users.Remove(oldUser);
        users.Add(user);

        return await Task.FromResult(true);
    }

        Task<User> IUserDataStore<User>.AuthenticateUserAsync(AuthenticationModel model)
        {
            throw new NotImplementedException();
        }
    }
}

