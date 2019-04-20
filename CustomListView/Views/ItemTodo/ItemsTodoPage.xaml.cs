using System;
using Xamarin.Forms;
using CustomListView.Models;
using CustomListView.ViewModels;
using CustomListView.ViewModels.ItemTodo;

namespace CustomListView.Views.ItemTodo
{
    public partial class ItemsTodoPage : ContentPage
    {
        ItemsTodoViewModel viewModel;

        public ItemsTodoPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsTodoViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Models.ItemTodo;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemTodoDetailPage(new ItemTodoDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemTodoPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.ItemsTodo.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}