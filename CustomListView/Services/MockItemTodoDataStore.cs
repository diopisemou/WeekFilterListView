using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomListView.Models;

namespace CustomListView.Services
{
    public class MockItemTodoDataStore : IItemTodoDataStore<ItemTodo>
    {
        List<ItemTodo> items;

        public MockItemTodoDataStore()
        {
            items = new List<ItemTodo>();
            for (int i = 1; i < 53; i++)
            {
                DateTime duedate = new DateTime();
                duedate = duedate.AddDays(((i * 7) - 4)-1);
                var mockItems = new List<ItemTodo>
                {
                    

                    new ItemTodo { Id = Guid.NewGuid().ToString(), Text = "First item of week "+i, Description="This is an item description." ,CreatedDate = DateTime.Now,UpdatedDate = DateTime.Now,IsDone = false ,DueDate = duedate },
                    new ItemTodo { Id = Guid.NewGuid().ToString(), Text = "Second item of week "+i, Description="This is an item description." ,CreatedDate = DateTime.Now,UpdatedDate = DateTime.Now,IsDone = false ,DueDate = duedate},
                    new ItemTodo { Id = Guid.NewGuid().ToString(), Text = "Third item of week "+i, Description="This is an item description." ,CreatedDate = DateTime.Now,UpdatedDate = DateTime.Now,IsDone = false ,DueDate = duedate},
                    new ItemTodo { Id = Guid.NewGuid().ToString(), Text = "Fourth item of week "+i, Description="This is an item description." ,CreatedDate = DateTime.Now,UpdatedDate = DateTime.Now,IsDone = false ,DueDate = duedate},
                    new ItemTodo { Id = Guid.NewGuid().ToString(), Text = "Fifth item of week "+i, Description="This is an item description." ,CreatedDate = DateTime.Now,UpdatedDate = DateTime.Now,IsDone = false ,DueDate = duedate},
                    new ItemTodo { Id = Guid.NewGuid().ToString(), Text = "Sixth item of week "+i, Description="This is an item description." ,CreatedDate = DateTime.Now,UpdatedDate = DateTime.Now,IsDone = false ,DueDate = duedate}
                };

                foreach (var item in mockItems)
                {
                    items.Add(item);
                }
            }
        }

        public async Task<bool> AddItemTodoAsync(ItemTodo item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemTodoAsync(ItemTodo item)
        {
            var oldItem = items.Where((ItemTodo arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemTodoAsync(string id)
        {
            var oldItem = items.Where((ItemTodo arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ItemTodo> GetItemTodoAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }


        public async Task<IEnumerable<ItemTodo>> GetItemsTodoAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<IEnumerable<ItemTodo>> GetItemsTodoAsync(DateTime timeline, bool forceRefresh = false)
        {
            double weeksnum = (double)52;
            double TimeLineDate = (double)timeline.DayOfYear ;
            double TimeLineDateNum = TimeLineDate / weeksnum;
            double TimeLineDateEndNum = ((TimeLineDate - 7)) / weeksnum;
            
            //List<ItemTodo> result = new List<ItemTodo>();
            //foreach (var item in items)
            //{
            //    if (item.IsDone == false)
            //    {
                    
            //        bool dutedatecheck = ((double)item.DueDate.DayOfYear / weeksnum >= TimeLineDateEndNum) && ((double)item.DueDate.DayOfYear / weeksnum <= TimeLineDateNum) ;
            //        if (dutedatecheck)
            //        {
            //            result.Add(item);
            //        }
            //    }
            //}

            return await Task.FromResult(items.Where(item => ((double)item.DueDate.DayOfYear / weeksnum >= TimeLineDateEndNum) && ((double)item.DueDate.DayOfYear / weeksnum <= TimeLineDateNum) && item.IsDone == false));

            //return await Task.FromResult(result);
            //yorslcr80j
        }
    }
}