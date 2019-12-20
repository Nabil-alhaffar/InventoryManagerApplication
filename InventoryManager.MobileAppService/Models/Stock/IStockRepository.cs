using System;
using System.Collections.Generic;

namespace InventoryManager.MobileAppService.Models
{
    public interface IStockRepository 
    {
        IEnumerable<Stock> GetAllStocks();
        IEnumerable<Stock> GetAllStoreStocks(int storeId);
        void AddStocks(int storeId, IEnumerable <Stock> stocks);
        void ReconcileInventory(int storeId, IEnumerable<Stock> stocks);

        void SubtractStocks(int storeId, IEnumerable<Stock> stocks);
        int GetProductCount(int storeId, int productId);
        //IEnumerable<Stock> GetAllStocksByProductType(int storeId);

        IEnumerable<Stock> GetAllPhoneStocks(int storeId);
        IEnumerable<Stock> GetAllAccessoriesStocks(int storeId);
        void AddSingleStock(int storeId, Stock stock);
        void SubtractSingleStock(int storeId, Stock stock);
        Stock GetStock(int storeId, int productId);
        void Update(Stock stock);


    }
}
