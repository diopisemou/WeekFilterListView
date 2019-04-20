using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CustomListView.Models;

namespace CustomListView.Views.ItemTodo
{
    public partial class NewItemTodoPage : ContentPage
    {
        public Models.ItemTodo Item { get; set; }

        public NewItemTodoPage()
        {
            InitializeComponent();

            Item = new Models.ItemTodo
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}