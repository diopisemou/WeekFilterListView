﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CustomListView.Services;
using CustomListView.Views;

namespace CustomListView
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;
        public static bool UseMockItemTodoDataStore = true;

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            if (UseMockItemTodoDataStore)
                DependencyService.Register<MockItemTodoDataStore>();
            else
                DependencyService.Register<AzureDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
