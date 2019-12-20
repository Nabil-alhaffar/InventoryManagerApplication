using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using InventoryManager.Models;
using System.Linq;

namespace InventoryManager.MobileAppService.Models
{
    public class StoreRepository : IStoreRepository
    {

        private static ConcurrentDictionary<string, Store> stores =
            new ConcurrentDictionary<string, Store>();
        private SqlDbContext dbContext;
        public StoreRepository(SqlDbContext _db)
        {
            dbContext = _db;

        }

        public IEnumerable<Store> GetAll() => dbContext.Stores.Include(s => s.Stocks).
                                                     ThenInclude(s => s.SerialNumbers).
                                                    Include(o => o.Orders).
                                                     ThenInclude(o => o.OrderItems).
                                                    Include(s => s.Users).
                                                    ToList();


        public void Add(Store store)
        {
            //store.Id = Guid.NewGuid().ToString();
            if (store != null)
            {
                dbContext.Stores.Add(store);
                dbContext.SaveChanges();
            }
        }
        public void Remove(int storeId)
        {
            Store store = dbContext.Stores.Find(storeId);
            dbContext.Stores.Remove(store);
        }


        public void Update(Store store)
        {
            dbContext.Stores.Update(store);
            dbContext.SaveChanges();

        }

        public Store Get(int id) => dbContext.Stores.Include(s=> s.Stocks).
                                                     ThenInclude(s => s.SerialNumbers).
                                                    Include(o=>o.Orders).
                                                    ThenInclude(o=> o.OrderItems).
                                                    Include(s=>s.Users).
                                                    FirstOrDefault(i=>i.StoreId == id);

        public Store GetStoreByStoreName (string storeName)=> dbContext.Stores.
                                                    Include(s => s.Stocks).
                                                    ThenInclude(s => s.SerialNumbers).
                                                    Include(o => o.Orders).
                                                    ThenInclude(o => o.OrderItems).
                                                    Include(s => s.Users).
                                                    FirstOrDefault(i=>i.StoreName.Equals(storeName));
     
    }
}
    //    public Dictionary<Product, int>  GetAllStocks(string storeId)
    //    {
    //        return stores[storeId].Stocks;
    //    }

    //    public void UpdateStocks(string storeId, Dictionary<Product,int> stocks)
    //    {
    //        stores[storeId].Stocks = stocks;

    //    }


    //    public void AddStocks(string storeId, Dictionary<Product, int> stocks)
    //    {
    //        foreach (Product item in stocks.Keys){
    //            if (!stores[storeId].Stocks.ContainsKey(item))
    //                stores[storeId].Stocks.TryAdd(item, stocks[item]);
    //            else 
    //                stores[storeId].Stocks[item] += stocks[item];
    //        }
          
    //    }

    //    public void SubtractStocks(string storeId, Dictionary<Product, int> stocks)
    //    {
    //        foreach (Product item in stocks.Keys)
    //        {
    //            if (stores[storeId].Stocks.ContainsKey(item))
    //                stores[storeId].Stocks[item] -= stocks[item];        
    //        }
    //    }

    //    public int GetProductCount(string storeId, Product item)
    //    {
    //        if (stores[storeId].Stocks.ContainsKey(item))
    //        {
    //            return stores[storeId].Stocks[item];
    //        }
    //        else
    //            return 0;
    //    }

    //    public Dictionary<Product, int> GetAllPhoneStocks(string storeId)
    //    {
    //        Dictionary<Product, int> phoneStocks = new Dictionary<Product, int>();
    //        foreach (Product item in stores[storeId].Stocks.Keys) {
    //            if (item.Category == ProductType.PhoneItem)
    //                phoneStocks.Add(item, stores[storeId].Stocks[item]);
    //        }
    //        return phoneStocks;
    //    }

    //    public Dictionary<Product, int> GetAllAccessoriesStocks(string storeId)
    //    {
    //        Dictionary<Product, int> accessoryStocks = new Dictionary<Product, int>();
    //        foreach (Product item in stores[storeId].Stocks.Keys)
    //        {
    //            if (item.Category == ProductType.AccessoryItem)
    //                accessoryStocks.Add(item, stores[storeId].Stocks[item]);
    //        }
    //        return accessoryStocks;
    //    }
    //}


