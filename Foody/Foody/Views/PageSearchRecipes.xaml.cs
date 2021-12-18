using Foody.Models;
using Foody.Views.DetailsRecipe;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Foody.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSearchRecipes : ContentPage
    {
        
        public PageSearchRecipes(Recipe results)
        {
            InitializeComponent();
            BindingContext = results;
        }
        

        private void BackToPantry_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pantry());
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