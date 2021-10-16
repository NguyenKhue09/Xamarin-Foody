using Foody.ViewModels;
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
    public partial class Pantry : ContentPage
    {
        private readonly PantryViewModel pantryViewModel;
        public Pantry()
        {
            InitializeComponent();
            CheckFavorite(true);
            BindingContext = pantryViewModel = new PantryViewModel();
        }

        void CheckFavorite(bool x)
        {
            if (x)
            {

                lb.Height = new GridLength(0.4, GridUnitType.Star);
                col.Height = new GridLength(0.98, GridUnitType.Star);
            }
            else
            {
                lb.Height = 0;
                col.Height = 0;
            }

        }
        private void TabPantry_SelectedTabIndexChanged(object sender, SelectedPositionChangedEventArgs e)
        {
            if (TabPantry.SelectedIndex == 0)
            {
                headPantry.HeightRequest = 180;
                pantry.IsVisible = false;
                ptManage.IsVisible = true;
                tabPantry.Height = new GridLength(0.4, GridUnitType.Star);
            }
            else
            {
                headPantry.HeightRequest = 80;
                pantry.IsVisible = true;
                ptManage.IsVisible = false;
                tabPantry.Height = new GridLength(0.33, GridUnitType.Star);
            }
        }
    }
}