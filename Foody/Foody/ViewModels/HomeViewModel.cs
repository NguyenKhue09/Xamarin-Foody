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
        public ObservableRangeCollection<Food> Foods { get; set; }
        public ObservableRangeCollection<Result> Recipes { get; set; }
        public ObservableRangeCollection<Result> RandomRecipes { get; set; }
        public ObservableRangeCollection<Result> PopularRecipes { get; set; }
        public List<Result> FavoriteRecipes { get; set; }


        public ICommand NavToPantry => new Command(NavToPantryPage);
        public ICommand NavToShoppingList => new Command(NavToShoppingListPage);
        public ICommand NavToMealPlanner => new Command(NavToMealPlannerPage);
        public HomeViewModel()
        {

            Foods = new ObservableRangeCollection<Food>();
            Recipes = new ObservableRangeCollection<Result>();
            RandomRecipes = new ObservableRangeCollection<Result>();
            FavoriteRecipes = new List<Result>();
            Foods.AddRange(Repository.Foods);

           
        }

        async public void GetRecipes()
        {
            Recipe results = await App.RecipeManager.GetRecipes();
            Recipes.AddRange(results.results);
        }

        async public void GetRandomRecipes()
        {
            Recipe results = new Recipe();

            results = await App.RecipeManager.GetRandomRecipes();
            RandomRecipes.AddRange(results.results);
            
        }

        async public Task<List<Result>> GetAllFavoriteRecipes()
        {
            RecipeDatabase recipeDatabase = await RecipeDatabase.Instance;

            List<Result> results = new List<Result>();

            List<FavoriteRecipe> favoriteRecipes = await recipeDatabase.GetAllFavoriteRecipes();
            foreach(FavoriteRecipe favoriteRecipe in favoriteRecipes)
            {
                Result result = JsonConvert.DeserializeObject<Result>(favoriteRecipe.JsonRecipe);
                Debug.WriteLine(result.id);
                results.Add(result);
            }

            return results;
        }

        async public void NavToPantryPage()
        {

            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/pantry", true);
           
        }

        async public void NavToShoppingListPage()
        {

            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/shoppingList", true);
        }
        async public void NavToMealPlannerPage()
        {

            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync("//tabbar/mealPlanner", true);
        }

        
    }
}
