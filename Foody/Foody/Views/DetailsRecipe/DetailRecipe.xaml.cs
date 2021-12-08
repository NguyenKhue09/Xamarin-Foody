using Foody.Data.Local;
using Foody.Models;
using Foody.Models.Local;
using Foody.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
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
            //detailRecipeViewModel.getIngredient();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsFavoriteRecipe();
        }

        async private void BackPress_Tapped(object sender, EventArgs e)
        {

            try
            {
                //Code to execute on tapped event
                //await(App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/home", true);
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void TabHost_SelectedTabIndexChanged(object sender, SelectedPositionChangedEventArgs e)
        {

        }

        private async void AddItemToShoppingListAsync(object sender, EventArgs e)
        {
            bool result = await detailRecipeViewModel.AddIngredientsToShoppingList();
            var messageOptions = new MessageOptions
            {
                Foreground = Color.Black,
                Font = Font.SystemFontOfSize(16),
                Message = result ? "Add to list successfully!" : "Add to list fail!"
            };

            var options = new SnackBarOptions
            {
                MessageOptions = messageOptions,
                Duration = TimeSpan.FromMilliseconds(3000),
                BackgroundColor = result ? Color.FromRgb(75, 181, 67) : Color.FromRgb(250, 17, 61),
                IsRtl = false,
            };
            await this.DisplaySnackBarAsync(options);
        }

        private async void AddRecipeToFavorite(object sender, EventArgs e)
        {
            detailRecipeViewModel.IsFavoriteRecipe = !detailRecipeViewModel.IsFavoriteRecipe;

            Debug.WriteLine(detailRecipeViewModel.recipe.id);

            FavoriteRecipe favoriteRecipe = new FavoriteRecipe
            {
                JsonRecipe = JsonConvert.SerializeObject(detailRecipeViewModel.recipe),
                RecipeId = detailRecipeViewModel.recipe.id
            };

            RecipeDatabase recipeDatabase = await RecipeDatabase.Instance;

            if (detailRecipeViewModel.IsFavoriteRecipe)
            {
                FavoriteIcon.Source = "heart_red.png";
                int x = await recipeDatabase.AddFavoriteRecipe(favoriteRecipe);
                Debug.WriteLine($"Insert in {x}");
            } else
            {
                FavoriteIcon.Source = "heart_outline.png";
                FavoriteRecipe deleteFavoriteRecipe = await recipeDatabase.GetFavoriteRecipes(detailRecipeViewModel.recipe.id);
                int x = await recipeDatabase.DeleteFavoriteRecipe(deleteFavoriteRecipe);
                Debug.WriteLine($"Delete in {x}");
            }
        }

        private async void IsFavoriteRecipe()
        {
            RecipeDatabase recipeDatabase = await RecipeDatabase.Instance;

            FavoriteRecipe favoriteRecipe = await recipeDatabase.GetFavoriteRecipes(detailRecipeViewModel.recipe.id);

            if(favoriteRecipe != null)
            {
                Debug.WriteLine(favoriteRecipe.RecipeId);
                detailRecipeViewModel.IsFavoriteRecipe = true;
                FavoriteIcon.Source = "heart_red.png";
            } else
            {
                detailRecipeViewModel.IsFavoriteRecipe = false;
                FavoriteIcon.Source = "heart_outline.png";
            }
        }
    }
}