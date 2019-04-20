using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomListView.Services
{
    public interface IItemTodoDataStore<T>
    {
        Task<bool> AddItemTodoAsync(T item);
        Task<bool> UpdateItemTodoAsync(T item);
        Task<bool> DeleteItemTodoAsync(string id);
        Task<T> GetItemTodoAsync(string id);
        Task<IEnumerable<T>> GetItemsTodoAsync(bool forceRefresh = false);

        Task<IEnumerable<T>> GetItemsTodoAsync(DateTime timeline, bool forceRefresh = false);
    }
}
