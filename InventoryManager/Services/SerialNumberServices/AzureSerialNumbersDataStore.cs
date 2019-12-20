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
    public class AzureSerialNumbersDataStore :ISerialNumbersDataStore<SerialNumber>
    {
        HttpClient client;
        IEnumerable<SerialNumber> serialNumbers;

        public AzureSerialNumbersDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");
            if (App.currentUser != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", App.currentUser.Token);
            }
            serialNumbers = new List<SerialNumber>();
        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<bool> AddSerialNumberAsync(SerialNumber serialNumber)
        {
            if (serialNumber == null || !IsConnected)
                return false;

            var serializedStock = JsonConvert.SerializeObject(serialNumber);

            var response = await client.PostAsync($"api/serialNumber", new StringContent(serializedStock, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteSerialNumberAsync(string storeId, string productId)
        {
            if (string.IsNullOrEmpty(storeId) && string.IsNullOrEmpty(productId) && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/serialNumber/{storeId}/{productId}");

            return response.IsSuccessStatusCode;
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SerialNumber>> GetAllSerialNumbersAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/serialNumber");
                serialNumbers = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<SerialNumber>>(json));
            }

            return serialNumbers;
        }

        public async Task<SerialNumber> GetSerialNumberAsync(string storeId, string productId)
        {
            if (storeId != null && productId != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/serialNumber/{storeId}/{productId}");
                return await Task.Run(() => JsonConvert.DeserializeObject<SerialNumber>(json));
            }

            return null;
        }

        public async Task<bool> UpdateSerialNumberAsync(SerialNumber serialnumber)
        {
            if (serialnumber == null || serialnumber.StoreId == null || !IsConnected)
                return false;

            var serializedStock = JsonConvert.SerializeObject(serialnumber);
            var buffer = Encoding.UTF8.GetBytes(serializedStock);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/serialNumber/{serialnumber.StoreId}/{serialnumber.ProductId}"), byteContent);

            return response.IsSuccessStatusCode;
        }
    }
}
