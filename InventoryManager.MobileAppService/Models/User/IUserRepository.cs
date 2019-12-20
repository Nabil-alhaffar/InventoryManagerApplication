using System;
using System.Collections.Generic;

namespace InventoryManager.MobileAppService.Models
{
    public interface IUserRepository
    {

        void Add(User user);
        void Update(User user);
        User Remove(int key);
        User Get(int id);
        IEnumerable<User> GetAll();
        User Authenticate(string username, string password, string StoreId);

    }
}
