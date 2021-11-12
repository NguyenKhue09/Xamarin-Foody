using Foody.Data.Local;
using Foody.Models;
using Foody.ViewModels;
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
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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

            RecipeDatabase recipeDatabase = await RecipeDatabase.Instance;

            if (detailRecipeViewModel.IsFavoriteRecipe)
            {
                FavoriteIcon.Source = "heart_red.png";
                //await recipeDatabase.AddFavoriteRecipe();
            } else
            {
                FavoriteIcon.Source = "heart_outline.png";
                //await recipeDatabase.DeleteFavoriteRecipe();
            }
        }
    }
}