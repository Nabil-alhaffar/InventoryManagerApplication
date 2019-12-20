using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventoryManager.Models;
namespace InventoryManager.Services
{
    public interface IUserDataStore<T>
    {

        Task <bool> AddUserAsync(T user);
        Task<bool> DeleteUserAsync(int userId);
        Task<bool> UpdateUserAsync(T user);
        Task<T> GetUserAsync(int userName);
        Task<IEnumerable<T>> GetUsersAsync(bool forceRefresh = false);
        Task<T> AuthenticateUserAsync(AuthenticationModel model);

    }
}
