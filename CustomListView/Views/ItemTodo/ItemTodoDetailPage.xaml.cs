using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CustomListView.Models;
using CustomListView.ViewModels;
using CustomListView.ViewModels.ItemTodo;

namespace CustomListView.Views.ItemTodo
{
    public partial class ItemTodoDetailPage : ContentPage
    {
        ItemTodoDetailViewModel viewModel;

        public ItemTodoDetailPage(ItemTodoDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemTodoDetailPage()
        {
            InitializeComponent();

            var item = new Models.ItemTodo
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemTodoDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}