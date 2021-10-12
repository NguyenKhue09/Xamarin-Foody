using Foody.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Foody.Views.DetailsRecipe
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailRecipe : ContentPage
    {
        public DetailRecipe(Models.Food food)
        {
            InitializeComponent();
            BindingContext = food;
            
        }


        async private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                //Code to execute on tapped event
                await(App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/home", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void TabHost_SelectedTabIndexChanged(object sender, SelectedPositionChangedEventArgs e)
        {
            if (TabHost.SelectedIndex != 0 )
            {
                faketab1.IsVisible = false;
            }
        }

        private void scroll_SizeChanged(object sender, EventArgs e)
        {
        }
            
    }
}