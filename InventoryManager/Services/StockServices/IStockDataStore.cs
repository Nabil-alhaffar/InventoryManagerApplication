using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManager.Services
{
    public interface IStockDataStore<T>
    {
        Task<bool> AddStockAsync(T stock);
        Task<bool> DeleteStockAsync(string storeId, string productId);
        Task<bool> UpdateStockAsync(T stock);
        Task<T> GetStockAsync(string storeId,string productId);
        Task<IEnumerable<T>> GetAllStocksAsync(bool forceRefresh = false);
    }
}
