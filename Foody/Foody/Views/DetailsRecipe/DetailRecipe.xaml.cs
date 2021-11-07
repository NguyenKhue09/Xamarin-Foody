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
            //var newItemShoppingList = new ItemShoppingList
            //{
            //    aisle = "Baking",
            //    item = "3 package baking powder",
            //};

            //detailRecipeViewModel.AddIngredientsToShoppingList(newItemShoppingList);

            //ShoppingListResult shoppingList = new ShoppingListResult();

            //shoppingList = await App.RecipeManager.GetShoppingList();

            //Debug.WriteLine(shoppingList.cost.ToString());

            //await App.RecipeManager.DeleteShoppingListItem("877471");

            //IngredientInform ingredientInform = new IngredientInform();

            //ingredientInform = await App.RecipeManager.GetIngredientInform("18369");

            //Debug.WriteLine(ingredientInform.image.ToString());

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

       
    }
}