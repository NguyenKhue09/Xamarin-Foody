using Foody.ViewModels;
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

        private readonly HomeViewModel homeViewModel;
        public Home()
        {
            InitializeComponent();
            BindingContext = homeViewModel = new HomeViewModel();

        }
        async void OnImageNameTapped(object sender, EventArgs args)
        {
            try
            {
                //Code to execute on tapped event
                await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/mealPlanner", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}