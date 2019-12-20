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
    public class AzureStockDataStore: IStockDataStore<Stock>
    {
        HttpClient client;
        IEnumerable<Stock> stocks;
        public AzureStockDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");
            if (App.currentUser != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.currentUser.Token);
            }
            stocks = new List<Stock>();
        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<bool> AddStockAsync(Stock stock)
        {
            if (stock == null || !IsConnected)
                return false;

            var serializedStock = JsonConvert.SerializeObject(stock);

            var response = await client.PostAsync($"api/Stocks", new StringContent(serializedStock, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteStockAsync(string storeId, string productId)
        {
            if (string.IsNullOrEmpty(storeId)&& string.IsNullOrEmpty(productId) && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/stocks/{storeId}/{productId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<Stock> GetStockAsync(string storeId, string productId)
        {
            if (storeId != null && productId!= null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/stocks/{storeId}/{productId}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Stock>(json));
            }

            return null;
        }

        public async Task<IEnumerable<Stock>> GetAllStocksAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/stocks");
                stocks = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Stock>>(json));
            }

            return stocks;
        }

        public async Task<bool> UpdateStockAsync(Stock stock)
        {
            if (stock == null || stock.StoreId == null || !IsConnected)
                return false;

            var serializedStock = JsonConvert.SerializeObject(stock);
            var buffer = Encoding.UTF8.GetBytes(serializedStock);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/stocks/{stock.StoreId}/{stock.ProductId}"), byteContent);

            return response.IsSuccessStatusCode;
        }

    }
}
