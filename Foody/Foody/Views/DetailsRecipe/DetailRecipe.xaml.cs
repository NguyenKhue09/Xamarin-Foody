using Foody.Models;
using Foody.ViewModels;
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
        private readonly DetailRecipeViewModel detailRecipeViewModel;
        public DetailRecipe(Result recipe)
        {
            InitializeComponent();
            BindingContext = detailRecipeViewModel = new DetailRecipeViewModel(recipe);
            //LstInstructions.ItemsSource = recipe.analyzedInstructions[0].steps;
           
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

        }

      
    }
}