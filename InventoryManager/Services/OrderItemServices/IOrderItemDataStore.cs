using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManager.Services
{
    public interface IOrderItemDataStore<T>
    {
        Task<bool> AddOrderItemAync(T orderItem);
        Task<bool> UpdateOrderItemAsync(T orderItem);
        Task<bool> DeleteOrderItemAsync(int id);
        Task<T> GetOrderItemAsync(int id);
        Task<IEnumerable<T>> GetOrderItemsAsync(bool forceRefresh = false);
    }
}

