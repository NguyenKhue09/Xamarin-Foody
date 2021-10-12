using Foody.Data;
using Foody.Services.RecipeApiCall;
using Foody.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Foody
{
    public partial class App : Application
    {
        public static RecipesManager RecipeManager { get; private set; }
        public App()
        {
            RecipeManager = new RecipesManager(new RestService());
            InitializeComponent();
            MainPage = new AppShell();
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
