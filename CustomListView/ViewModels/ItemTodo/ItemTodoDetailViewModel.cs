using System;

using CustomListView.Models;

namespace CustomListView.ViewModels.ItemTodo
{
    public class ItemTodoDetailViewModel : BaseViewModel
    {
        public Models.ItemTodo Item { get; set; }
        public ItemTodoDetailViewModel(Models.ItemTodo item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
