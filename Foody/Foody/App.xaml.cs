using Foody.Data;
using Foody.Models;
using Foody.Services.RecipeApiCall;
using Foody.ViewModels;
using Foody.Views;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Foody
{
    public partial class App : Application
    {
        public static RecipesManager RecipeManager { get; private set; }

        private static LoginViewModel loginViewModel;
        public static LoginViewModel LoginViewModel
        {
            get
            {
                if (loginViewModel == null)
                    loginViewModel = new LoginViewModel();
                return loginViewModel;
            }
        }
        public App()
        {
            RecipeManager = new RecipesManager(new RestService());
            InitializeComponent();
            MainPage = new AppShell();
            Routing.RegisterRoute("Login", typeof(Login));
            LoginViewModel.CheckUserLogin();
        }

        protected async override void OnStart()
        {
            if(!LoginViewModel.isLogin)
            {
                await Shell.Current.GoToAsync("Login", true);
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
