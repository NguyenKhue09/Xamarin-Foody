using Foody.Models;
using Foody.ViewModels;
using Foody.Views.DetailsRecipe;
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
    public partial class FavoriteRecipesPage : ContentPage
    {
        private readonly HomeViewModel homeViewModel;
        public FavoriteRecipesPage()
        {
            InitializeComponent();
            BindingContext = homeViewModel = new HomeViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            homeViewModel.FavoriteRecipes = await homeViewModel.GetAllFavoriteRecipes();
            if (homeViewModel.FavoriteRecipes.Count > 0)
            {
                favorite_Recipes_Foody.ItemsSource = homeViewModel.FavoriteRecipes;
                row1.Height = 0;
                row2.Height = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                row1.Height = new GridLength(1, GridUnitType.Star);
                row2.Height = 0;
            }
        }

        private async void favorite_Recipes_Foody_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (favorite_Recipes_Foody.SelectedItem != null)
            {
               
                Result recipe = (Result)favorite_Recipes_Foody.SelectedItem;
                recipe.SelectedViewModelIndex = 0;
                await Navigation.PushAsync(new DetailRecipe(recipe));
            }

            favorite_Recipes_Foody.SelectedItem = null;
        }
    }
}