using System;
using System.Collections.Generic;
using InventoryManager.MobileAppService.Models;

namespace InventoryManager.Models
{
    public interface IStoreRepository
    {

        void Add(Store store);
        IEnumerable<Store> GetAll();
        void Update(Store store);
        Store Get(int id);
        void Remove(int id);
        Store GetStoreByStoreName(string storeName);

        //Dictionary<Product, int>  GetAllStocks(string storeId);
        //void AddStocks(string storeId, Dictionary<Product, int> stocks);
        //void SubtractStocks(string storeId, Dictionary<Product, int> stocks);
        //int GetProductCount(string storeId, Product item );
        //Dictionary<Product, int> GetAllPhoneStocks(string storeId);
        //Dictionary<Product, int> GetAllAccessoriesStocks(string storeId);
      //void UpdateStocks(string storeId, Dictionary<Product, int> stocks);

    }
}
