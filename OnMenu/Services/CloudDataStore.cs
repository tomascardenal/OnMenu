using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using OnMenu.Models;
using Plugin.Connectivity;

namespace OnMenu
{
    public class CloudDataStore : IDataStore<Models.Ingredient>
    {
        HttpClient client;
        IEnumerable<Models.Ingredient> items;

        public CloudDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.BackendUrl}/");

            items = new List<Models.Ingredient>();
        }

        public async Task<IEnumerable<Models.Ingredient>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/ingredient");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Models.Ingredient>>(json));
            }

            return items;
        }

        public async Task<Models.Ingredient> GetItemAsync(string name)
        {
            if (name != null && CrossConnectivity.Current.IsConnected)
            {
                var json = await client.GetStringAsync($"api/ingredient/{name}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Models.Ingredient>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Models.Ingredient ingredient)
        {
            if (ingredient == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(ingredient);

            var response = await client.PostAsync($"api/ingredient", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Models.Ingredient ingredient)
        {
            if (ingredient == null || ingredient.Name == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(ingredient);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/ingredient/{ingredient.Name}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string name)
        {
            if (string.IsNullOrEmpty(name) && !CrossConnectivity.Current.IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/ingredient/{name}");

            return response.IsSuccessStatusCode;
        }
    }
}
