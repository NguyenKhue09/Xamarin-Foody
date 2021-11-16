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

        async private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/home", true);
        }

        async private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/pantry", true);
        }

        async private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            await(App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/shoppingList", true);
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FavoriteRecipesPage());
        }

        async private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            await(App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/mealPlanner", true);
        }

        private void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Account());
        }

        private void TapGestureRecognizer_Tapped_6(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }
    }
}