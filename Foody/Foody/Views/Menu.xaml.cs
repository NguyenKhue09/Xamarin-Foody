using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Foody.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            InitializeComponent();
        }
        async private void MenuToHome_Tapped(object sender, EventArgs e)
        {
            await (Application.Current.MainPage as Shell).GoToAsync("//tabbar/home", true);
        }

        async private void MenuToPantry_Tapped(object sender, EventArgs e)
        {
            await (Application.Current.MainPage as Shell).GoToAsync("//tabbar/pantry", true);
        }

        async private void MenuToShoppingList_Tapped(object sender, EventArgs e)
        {
            await(Application.Current.MainPage as Shell).GoToAsync("//tabbar/shoppingList", true);
        }

        private async void MenuToFavortiteRecipes_Tapped(object sender, EventArgs e)
        {
            await(Application.Current.MainPage as Shell).GoToAsync("//tabbar/menu/FavoriteRecipes", true);
        }

        async private void MenuToMealPlanner_Tapped(object sender, EventArgs e)
        {
            await(Application.Current.MainPage as Shell).GoToAsync("//tabbar/mealPlanner", true);
        }

        private async void MenuToAccount_Tapped(object sender, EventArgs e)
        {
            await(Application.Current.MainPage as Shell).GoToAsync("//tabbar/menu/Account", true);
        }

        private void Logout_Tapped(object sender, EventArgs e)
        {
            App.LoginViewModel.UserLogout();
        }
    }
}