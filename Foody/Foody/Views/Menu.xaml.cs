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
            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/home", true);
        }

        async private void MenuToPantry_Tapped(object sender, EventArgs e)
        {
            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/pantry", true);
        }

        async private void MenuToShoppingList_Tapped(object sender, EventArgs e)
        {
            await(App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/shoppingList", true);
        }

        private void MenuToFavortiteRecipes_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FavoriteRecipesPage());
        }

        async private void MenuToMealPlanner_Tapped(object sender, EventArgs e)
        {
            await(App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/mealPlanner", true);
        }

        private void MenuToAccount_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Account());
        }

        private void Logout_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateAccount());
        }
    }
}