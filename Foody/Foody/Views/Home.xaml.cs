using Foody.Models;
using Foody.ViewModels;
using Foody.Views.DetailsRecipe;
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
            CheckFavorite(true);
            BindingContext = homeViewModel = new HomeViewModel();
            

        }

        

        void CheckFavorite(bool x)
        {
            if (x)
            {
                
                lb.Height = new GridLength(0.98, GridUnitType.Star);
                col.Height = new GridLength(0.3, GridUnitType.Star);
            }
            else
            {
                lb.Height = 0;
                col.Height = 0;
            }
            
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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //homeViewModel.GetRecipes();
            homeViewModel.GetRandomRecipes();
        }



        //async private void favorite_Recipes_Foody_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (favorite_Recipes_Foody.SelectedItem != null)
        //    {
        //        Debug.WriteLine("Choose");
        //        Result recipe = (Result)favorite_Recipes_Foody.SelectedItem;
        //        recipe.SelectedViewModelIndex = 0;
        //        await Navigation.PushAsync(new DetailRecipe(recipe));
        //    }

        //    favorite_Recipes_Foody.SelectedItem = null;
        //}

        async private void collectionView_Popular_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void random_recipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

    }
}