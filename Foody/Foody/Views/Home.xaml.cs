using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Foody.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {

        public ICommand NavToPantry => new Command(NavToPantryPage);
        public ICommand NavToShoppingList => new Command(NavToShoppingListPage);
        public ICommand NavToMealPlanner => new Command(NavToMealPlannerPage);
        public Home()
        {
            InitializeComponent();
            BindingContext = this;
           
        }

        async public void NavToPantryPage()
        {
           
            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/pantry", true);
        }

        async public void NavToShoppingListPage()
        {

            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/shoppingList", true);
        }
        async public void NavToMealPlannerPage()
        {

            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/mealPlanner", true);
        }

        async void OnImageNameTapped(object sender, EventArgs args)
        {
            try
            {
                //Code to execute on tapped event
                await(App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/mealPlanner", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}