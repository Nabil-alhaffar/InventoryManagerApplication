using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManager.Models;
using InventoryManager.Services;
using Microsoft.EntityFrameworkCore;
namespace InventoryManager.Services
{
    public class StoreMockDataStore:DbContext, IStoreDataStore<Store,Stock>
    {
      //  List<Store> stores;
        List<Stock> items;

        public DbSet<Store> stores { get; set; }


        public StoreMockDataStore()
        {

            Database.EnsureCreated();

        }
        string sqlitePath = App.sqliteURL;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename = {sqlitePath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Store>()
          //  .Property.
        }


        public async Task<IEnumerable<Stock>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
        public async Task<IEnumerable<Store>> GetStoresAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(stores);
        }
        public async Task<bool> UpdateItemAsync(Stock item)
        {
            var oldItem = items.Where((Stock arg) => arg.StoreId == item.StoreId).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }
        public async Task<bool> UpdateStoreAsync(Store store)
        {
            var oldStore = stores.Where((Store arg) => arg.StoreId == store.StoreId).FirstOrDefault();
            stores.Remove(oldStore);
            stores.Add(store);

            return await Task.FromResult(true);
        }

        public Task<bool> AddStoreAsync(Store store)
        {
            throw new NotImplementedException();
        }

        public Task<Store> GetStoreAsync(string storeId, bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStoreStocksAsync(string storeId, IEnumerable<Stock> stocks)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Stock>> GetAllStocksAsync(string storeId, bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddStocksAsync(string storeId, IEnumerable<Stock> stocks)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubtractStocksAsync(string storeId, IEnumerable<Stock> stocks)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetStockCountAsync(string storeId, string productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Stock>> GetAllPhoneStocksAsync(string storeId, bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Stock>> GetAllAccessoryStocksAsync(string storeId, bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Stock>> GetStoreStocksAsync(string storeId, bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<Store> GetStoreAsync(int storeId, bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStoreStocksAsync(int storeId, IEnumerable<Stock> stocks)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Stock>> GetAllStocksAsync(int storeId, bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddStocksAsync(int storeId, IEnumerable<Stock> stocks)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SubtractStocksAsync(int storeId, IEnumerable<Stock> stocks)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetStockCountAsync(int storeId, int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Stock>> GetAllPhoneStocksAsync(int storeId, bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Stock>> GetAllAccessoryStocksAsync(int storeId, bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Stock>> GetStoreStocksAsync(int storeId, bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<Store> GetStoreByStoreNameAsync(string StoreName, bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReconcileInventoryAsync(int storeId, IEnumerable<Stock> stocks)
        {
            throw new NotImplementedException();
        }
    }
}
