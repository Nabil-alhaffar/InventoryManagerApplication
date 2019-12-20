using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using InventoryManager.Models;

namespace InventoryManager.Services
{
    public class AzureUserDataStore : IUserDataStore<User>
    {
        HttpClient client;
        IEnumerable<User> users;

        public AzureUserDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            users = new List<User>();
        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<User>> GetUsersAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/user");
                users = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<User>>(json));
            }

            return users;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            if (user == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(user, new JsonSerializerSettings { NullValueHandling =NullValueHandling.Ignore, Converters = new List<JsonConverter> { new BadDateFixingConverter() } });

            var response = await client.PostAsync($"api/user", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateUserAsync(User user)
        {
            if (user == null || user.UserName == null || !IsConnected)
                return false;

            var serializedUser = JsonConvert.SerializeObject(user);
            var buffer = Encoding.UTF8.GetBytes(serializedUser);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/user/{user.UserName}"), byteContent);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteUserAsync(int userId)
        {
            if (userId == null  && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/user/{userId}");

            return response.IsSuccessStatusCode;
        }


        public async Task<User> GetUserAsync(int userId)
        {
            if (userId != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/user/{userId}");
                return await Task.Run(() => JsonConvert.DeserializeObject<User>(json));
            }

            return null;
        }
        public async Task <User> AuthenticateUserAsync(AuthenticationModel user)
        {
            if (user == null || !IsConnected)
                return null;
            string serializedItem = JsonConvert.SerializeObject(user).ToLowerInvariant();

            var response = await client.PostAsync($"api/user/authenticate", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            string responseBody = await response.Content.ReadAsStringAsync();

            return await Task.Run(() => JsonConvert.DeserializeObject<User>(responseBody));

            //return response.IsSuccessStatusCode;


        }
    }
}




