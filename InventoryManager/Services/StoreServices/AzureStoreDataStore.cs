using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using InventoryManager.Models;
using System.Net.Http.Headers;

namespace InventoryManager.Services
{
    public class AzureStoreDataStore : IStoreDataStore<Store, Stock>
    {
        HttpClient client;
        IEnumerable<Stock> stocks;
        IEnumerable<Store> stores;
        public AzureStoreDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");
            if (App.currentUser != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.currentUser.Token);
            }
            stores = new List<Store>();



        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<bool> AddStoreAsync(Store store)
        {

            if (store == null || !IsConnected)
                return false;

            var serializedStore = JsonConvert.SerializeObject(store);

            var response = await client.PostAsync($"api/store", new StringContent(serializedStore, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }


        public async Task<Store> GetStoreAsync(int storeId, bool forceRefresh = false)
        {
            if (storeId != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/store/{storeId}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Store>(json));
            }

            return null;

        }
        public async Task <Store> GetStoreByStoreNameAsync (string storeName, bool forceRefresh = false)
        {

            if (storeName != null && IsConnected)
            {
                
                var json = await client.GetStringAsync($"api/store/storeName?StoreName="+storeName);
                return await Task.Run(() => JsonConvert.DeserializeObject<Store>(json));
            }

            return null;
        }

        public async Task<IEnumerable<Store>> GetStoresAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/store");
                stores = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Store>>(json));
            }
            return stores;

        }
        public async Task<bool> UpdateStoreAsync(Store store)
        {

            if (store == null || store.StoreId == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(store);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/store/{store.StoreId}"), byteContent);

            return response.IsSuccessStatusCode;

        }

        public async Task<IEnumerable<Stock>> GetStoreStocksAsync(int storeId, bool forceRefresh = false)
        {
            forceRefresh = true;
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/store/{storeId}/stocks");
                stocks = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Stock>>(json));
            }

            return stocks;
        }
   

        public async Task<bool> AddStocksAsync(int storeId , IEnumerable< Stock> stocks)
        {
            if (stocks == null || storeId == null || !IsConnected)
                return false;

            var serializedStock = JsonConvert.SerializeObject(stocks);

            var response = await client.PostAsync($"api/store/{storeId}/stocks/", new StringContent(serializedStock, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateStoreStocksAsync(int storeId, IEnumerable <Stock>stocks)
        {
            if (stocks == null || storeId == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(stocks);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/store/{storeId}/stocks/"), byteContent);

            return response.IsSuccessStatusCode;
        }


        public async Task<IEnumerable<Stock>> GetAllStocksAsync(int storeId, bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/stocks/");
                stocks = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Stock>>(json));
            }

            return stocks;
        }

        public async Task<bool> ReconcileInventoryAsync(int storeId, IEnumerable<Stock> stocks) {

            if (stocks == null || storeId == null || !IsConnected)
                return false;
            var serializedStocks = JsonConvert.SerializeObject(stocks);
            var buffer = Encoding.UTF8.GetBytes(serializedStocks);
            var byteContent = new ByteArrayContent(buffer);
            var response = await client.PostAsync($"api/store/{storeId}/stocks/reconcile", new StringContent(serializedStocks, Encoding.UTF8, "application/json"));
            //var response = await client.DeleteAsync(new Uri($"api/store/{storeId}/stocks/subtract/"));

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> SubtractStocksAsync(int storeId, IEnumerable<Stock> stocks)
        {
            if (stocks == null || storeId == null || !IsConnected)
                return false;

            var serializedStocks = JsonConvert.SerializeObject(stocks);
            var buffer = Encoding.UTF8.GetBytes(serializedStocks);
            var byteContent = new ByteArrayContent(buffer);
            var response = await client.PostAsync($"api/store/{storeId}/stocks/subtract", new StringContent(serializedStocks, Encoding.UTF8, "application/json"));
            //var response = await client.DeleteAsync(new Uri($"api/store/{storeId}/stocks/subtract/"));

            return response.IsSuccessStatusCode;
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
    }
}






 