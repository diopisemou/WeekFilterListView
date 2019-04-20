using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using CustomListView.Models;

namespace CustomListView.Services
{
    public class AzureItemTodoDataStore : IItemTodoDataStore<ItemTodo>
    {
        HttpClient client;
        IEnumerable<ItemTodo> items;

        public AzureItemTodoDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            items = new List<ItemTodo>();
        }

        bool IsConnected => Connectivity.NetworkAccess != NetworkAccess.Internet;
        public async Task<IEnumerable<ItemTodo>> GetItemsTodoAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/item");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<ItemTodo>>(json));
            }

            return items;
        }

        public async Task<IEnumerable<ItemTodo>> GetItemsTodoAsync(DateTime timeline,bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/item/?parameter={timeline.DayOfYear}");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<ItemTodo>>(json));
            }

            return items;
        }

        public async Task<ItemTodo> GetItemTodoAsync(string id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<ItemTodo>(json));
            }

            return null;
        }

        public async Task<bool> AddItemTodoAsync(ItemTodo item)
        {
            if (item == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemTodoAsync(ItemTodo item)
        {
            if (item == null || item.Id == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemTodoAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/item/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}