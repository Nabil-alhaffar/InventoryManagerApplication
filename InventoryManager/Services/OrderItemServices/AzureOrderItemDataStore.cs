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
    public class AzureOrderItemDataStore : IOrderItemDataStore<OrderItems>
    {
        HttpClient client;
        IEnumerable<OrderItems> orderItems;
        public AzureOrderItemDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}");
            if (App.currentUser != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.currentUser.Token);
            }
            orderItems = new List<OrderItems>();
        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<bool> AddOrderItemAync(OrderItems orderItem)
        {
            if(orderItem == null || !IsConnected)
                return false;

            var serializedOrderItem = JsonConvert.SerializeObject(orderItem, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore , DateFormatString = "yyyy-MM-ddTH:mm:ss.fffZ" }).ToLower();

            var response = await client.PostAsync($"api/orderItem", new StringContent(serializedOrderItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOrderItemAsync(int id)
        {
            if (id == null && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/orderItem/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<OrderItems> GetOrderItemAsync(int id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/orderitem/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<OrderItems>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
            }

            return null;
        }

        public async Task<IEnumerable<OrderItems>> GetOrderItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {

                var json = await client.GetStringAsync($"api/orderitem");
                orderItems = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<OrderItems>>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
            }

            return orderItems;
        }

        public async Task<bool> UpdateOrderItemAsync(OrderItems orderItem)
        {
            if (orderItem == null || orderItem.OrderId == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(orderItems, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/orderitem/{orderItem.OrderId}"), byteContent);

            return response.IsSuccessStatusCode;
        }
    }
}
