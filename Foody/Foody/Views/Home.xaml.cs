using Foody.Models;
using Foody.ViewModels;
using Foody.Views.DetailsRecipe;
using MvvmHelpers;
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
                //await Navigation.PushAsync(new Account());
                await Shell.Current.GoToAsync("//tabbar/menu/Account", true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            homeViewModel.Recipes.Clear();
            homeViewModel.RandomRecipes.Clear();
            homeViewModel.FavoriteRecipes.Clear();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (App.LoginViewModel.ObsGoogleUser != null)
            {
                UserImage.Source = App.LoginViewModel.ObsGoogleUser.Picture;
                if (App.LoginViewModel.ObsGoogleUser.Name == null || App.LoginViewModel.ObsGoogleUser.Name == "")
                {
                    if (App.LoginViewModel.ObsGoogleUserDetails != null)
                    {
                        UserName.Text = $"Hi, {App.LoginViewModel.ObsGoogleUserDetails.Name}";
                    } else
                    {
                        UserName.Text = $"Hi, {App.LoginViewModel.ObsGoogleUser.Email}";
                    }
                } else
                {
                    UserName.Text = $"Hi, {App.LoginViewModel.ObsGoogleUser.Name}";
                }
               
            }

            homeViewModel.GetRecipes();
            homeViewModel.GetRandomRecipes();
            homeViewModel.FavoriteRecipes = await homeViewModel.GetAllFavoriteRecipes();
            
            if (homeViewModel.FavoriteRecipes.Count > 0)
            {
                favorite_Recipes_Foody.ItemsSource = homeViewModel.FavoriteRecipes;
                lb.Height = new GridLength(0.4, GridUnitType.Star);
                col.Height = new GridLength(0.98, GridUnitType.Star);
            }
            else
            {
                lb.Height = 0;
                col.Height = 0;
            }
        }

       
        async private void favorite_Recipes_Foody_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (favorite_Recipes_Foody.SelectedItem != null)
            {
                Debug.WriteLine("Choose");
                Result recipe = (Result)favorite_Recipes_Foody.SelectedItem;
                recipe.SelectedViewModelIndex = 0;
                await Navigation.PushAsync(new DetailRecipe(recipe));
            }

            favorite_Recipes_Foody.SelectedItem = null;
        }

        async private void collectionView_Popular_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (collectionView_Popular.SelectedItem != null)
            {
                Debug.WriteLine("Choose");
                Result recipe = (Result)collectionView_Popular.SelectedItem;
                recipe.SelectedViewModelIndex = 0;
                await Navigation.PushAsync(new DetailRecipe(recipe));
            }
            collectionView_Popular.SelectedItem = null;
        }

        async private void random_recipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (random_recipes.SelectedItem != null)
            {
                Debug.WriteLine("Choose");
                Result recipe = (Result)random_recipes.SelectedItem;
                recipe.SelectedViewModelIndex = 0;
                await Navigation.PushAsync(new DetailRecipe(recipe));
            }
            random_recipes.SelectedItem = null;
        }

    }
}