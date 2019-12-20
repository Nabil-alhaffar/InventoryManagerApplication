using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace InventoryManager.Services
{
    public interface IOrderDataStore <T>
    {
        Task<bool> AddOrderAync(T order);
        Task<bool> UpdateOrderAsync(T order);
        Task<bool> DeleteOrderAsync(int id);
        Task<T> GetOrderAsync(int id);
        Task<IEnumerable<T>> GetOrdersAsync(bool forceRefresh = false);
    }
}
