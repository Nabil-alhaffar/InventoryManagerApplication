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
    public class AzureOrderDataStore :IOrderDataStore<Order>
    {
        HttpClient client;
        IEnumerable<Order> orders;
        public AzureOrderDataStore()
        {

            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}");
            if (App.currentUser != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.currentUser.Token);
            }
            orders = new List<Order>();
        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<bool> AddOrderAync(Order order)
        {
            if (order == null || !IsConnected)
                return false;

            var serializedOrder = JsonConvert.SerializeObject(order, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Converters = new List<JsonConverter> { new BadDateFixingConverter() } });

            var response = await client.PostAsync($"api/order", new StringContent(serializedOrder, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteOrderAsync(int id)
        {
            if (id == null && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/order/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/order/{id}");
                return await Task.Run((Func<Order>)(() => JsonConvert.DeserializeObject<Order>((string)json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })));
            }

            return null;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {

                var json = await client.GetStringAsync($"api/order");
                orders = await Task.Run((Func<IEnumerable<Order>>)(() => JsonConvert.DeserializeObject<IEnumerable<Order>>((string)json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })));
            }

            return orders;
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            if (order == null || order.OrderId == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(order, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/order/{order.OrderId}"), byteContent);

            return response.IsSuccessStatusCode;
        }
    }
}
