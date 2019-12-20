using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace InventoryManager.Services
{
    public interface IStoreDataStore <T, U>
    {
        Task<bool> UpdateStoreAsync(T store);
        Task<IEnumerable<T>> GetStoresAsync(bool forceRefresh = false);
        Task<bool> AddStoreAsync(T store);
        Task<T> GetStoreAsync(int storeId, bool forceRefresh = false);
        Task<T> GetStoreByStoreNameAsync(string StoreName, bool forceRefresh = false);
        Task<bool> ReconcileInventoryAsync(int storeId, IEnumerable<U> stocks);
        Task<bool> UpdateStoreStocksAsync(int storeId, IEnumerable<U> stocks);
        Task<IEnumerable<U>> GetAllStocksAsync(int storeId, bool forceRefresh = false);
        Task<bool> AddStocksAsync(int storeId, IEnumerable<U> stocks);
        Task<bool> SubtractStocksAsync(int storeId, IEnumerable <U> stocks);
        Task<int> GetStockCountAsync(int storeId, int productId);
        Task <IEnumerable<U>> GetAllPhoneStocksAsync(int storeId, bool forceRefresh = false);
        Task<IEnumerable<U>> GetAllAccessoryStocksAsync(int storeId, bool forceRefresh = false);
        Task<IEnumerable<U>> GetStoreStocksAsync(int storeId, bool forceRefresh = false);



    }
}

