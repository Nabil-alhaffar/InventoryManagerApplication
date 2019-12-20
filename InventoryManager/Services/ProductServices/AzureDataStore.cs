using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using InventoryManager.Models;
using System.Net.Http.Headers;
using System.Linq;
namespace InventoryManager.Services
{
    public class AzureDataStore : IDataStore<Product>
    {
        HttpClient client;
        IEnumerable<Product> products;

        public AzureDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}");
            if (App.currentUser != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.currentUser.Token);
            }
            products = new List<Product>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<Product>> GetProductsAsync(bool forceRefresh = false)
        {
            if (products.Count() == 0)
                forceRefresh = true;
            if (forceRefresh && IsConnected)
            {

                var json = await client.GetStringAsync($"api/product");
                products = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Product>>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
            }

            return products;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/product/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Product>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
            }

            return null;
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            if (product == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject (product, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }).ToLower();

            var response = await client.PostAsync($"api/product", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            if (product == null || product.Id == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(product, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/product/{product.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            if (id == null && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/product/{id}");

            return response.IsSuccessStatusCode;
        }
       

    
    }
}
