using System;
using System.Collections.Generic;

namespace CustomListView.Models.ItemTodo
{
    public interface IItemTodoRepository
    {
        void Add(ItemTodo item);
        void Update(ItemTodo item);
        ItemTodo Remove(string key);
        ItemTodo Get(string id);
        IEnumerable<ItemTodo> GetAll();
    }
}
