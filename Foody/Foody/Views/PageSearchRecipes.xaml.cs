using Foody.Models;
using MvvmHelpers;
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
    public partial class PageSearchRecipes : ContentPage
    {
        public ObservableRangeCollection<Result> Recipes { get; set; }
        public PageSearchRecipes()
        {
            InitializeComponent();
            BindingContext = Recipes = new ObservableRangeCollection<Result>();
        }
        async public void GetRecipes()
        {
            Recipe results = new Recipe();

            results = await App.RecipeManager.GetRecipes();
            Recipes.AddRange(results.results);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pantry());
        }
    }
}