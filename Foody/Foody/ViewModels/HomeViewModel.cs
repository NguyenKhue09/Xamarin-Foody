using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Foody.Models;
using Foody.Services;
using System.Diagnostics;
using Foody.Data.Local;
using Foody.Models.Local;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Foody.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Result> Recipes { get; set; }
        public ObservableRangeCollection<Result> RandomRecipes { get; set; }
        public ObservableRangeCollection<Result> PopularRecipes { get; set; }
        public List<Result> FavoriteRecipes { get; set; }


        public ICommand NavToPantry => new Command(NavToPantryPage);
        public ICommand NavToShoppingList => new Command(NavToShoppingListPage);
        public ICommand NavToMealPlanner => new Command(NavToMealPlannerPage);
        public HomeViewModel()
        {
            Recipes = new ObservableRangeCollection<Result>();
            RandomRecipes = new ObservableRangeCollection<Result>();
            FavoriteRecipes = new List<Result>();
        }

        async public void GetRecipes()
        {
            Recipe results = await App.RecipeManager.GetPopularRecipes();
            if (results != null)
                Recipes.AddRange(results.results);
        }

        async public void GetRandomRecipes()
        {
            Recipe results = new Recipe();

            results = await App.RecipeManager.GetRandomRecipes();
            if(results != null)
                RandomRecipes.AddRange(results.results);
            
        }

        async public Task<List<Result>> GetAllFavoriteRecipes()
        {
            RecipeDatabase recipeDatabase = await RecipeDatabase.Instance;

            List<Result> results = new List<Result>();
            List<FavoriteRecipe> favoriteRecipes = new List<FavoriteRecipe>();
            if (App.LoginViewModel.GoogleUser != null)
            {
                favoriteRecipes = await recipeDatabase.GetAllFavoriteRecipes(App.LoginViewModel.GoogleUser.UID);
            }

            if(favoriteRecipes != null)
            {
                foreach (FavoriteRecipe favoriteRecipe in favoriteRecipes)
                {
                    Result result = JsonConvert.DeserializeObject<Result>(favoriteRecipe.JsonRecipe);
                    results.Add(result);
                }
            } else
            {
                results = null;
            }
            

            return results;
        }

        async public void NavToPantryPage()
        {

            await (Application.Current.MainPage as Shell).GoToAsync("//tabbar/pantry", true);
           
        }

        async public void NavToShoppingListPage()
        {

            await (Application.Current.MainPage as Shell).GoToAsync("//tabbar/shoppingList", true);
        }
        async public void NavToMealPlannerPage()
        {

            await (Application.Current.MainPage as Shell).GoToAsync("//tabbar/mealPlanner", true);
        }

        
    }
}
