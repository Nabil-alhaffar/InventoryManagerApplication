using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace InventoryManager.Services
{
    public interface ISerialNumbersDataStore<T>
    {
        Task<bool> AddSerialNumberAsync(T serialNumber);
        Task<bool> DeleteSerialNumberAsync(string storeId, string productId);
        Task<bool> UpdateSerialNumberAsync(T serialNumber);
        Task<T> GetSerialNumberAsync(string storeId, string productId);
        Task<IEnumerable<T>> GetAllSerialNumbersAsync(bool forceRefresh = false);
    }
}
