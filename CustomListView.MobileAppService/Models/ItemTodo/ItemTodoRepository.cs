using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CustomListView.Models.ItemTodo
{
    public class ItemTodoRepository : IItemTodoRepository
    {
        private static ConcurrentDictionary<string, ItemTodo> items =
            new ConcurrentDictionary<string, ItemTodo>();

        public ItemTodoRepository()
        {
            Add(new ItemTodo { Id = Guid.NewGuid().ToString(), Text = "Item 1", Description = "This is an item description." });
            Add(new ItemTodo { Id = Guid.NewGuid().ToString(), Text = "Item 2", Description = "This is an item description." });
            Add(new ItemTodo { Id = Guid.NewGuid().ToString(), Text = "Item 3", Description = "This is an item description." });
        }

        public ItemTodo Get(string id)
        {
            return items[id];
        }

        public IEnumerable<ItemTodo> GetAll()
        {
            return items.Values;
        }

        public void Add(ItemTodo item)
        {
            item.Id = Guid.NewGuid().ToString();
            items[item.Id] = item;
        }

        public Item Find(string id)
        {
            Item item;
            items.TryGetValue(id, out item);

            return item;
        }

        public ItemTodo Remove(string id)
        {
            ItemTodo item;
            items.TryRemove(id, out item);

            return item;
        }

        public void Update(ItemTodo item)
        {
            items[item.Id] = item;
        }
    }
}
