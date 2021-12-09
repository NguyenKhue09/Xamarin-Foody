using Foody.Models;
using Foody.Services.RecipeApiCall;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Data
{
    public class RecipesManager
    {
        RestService restService;

        public RecipesManager(RestService service)
        {
            restService = service;
        }

        public Task<Recipe> GetRecipes()
        {
            return restService.GetRecipes();
        }

        public Task<Recipe> GetRandomRecipes()
        {
            return restService.GetRandomRecipes();
        }

        public Task<Recipe> GetPopularRecipes()
        {
            return restService.GetPopularRecipes();
        }

        public Task<Recipe> SearchRecipes(string query, string cuisine, string intolerances)
        {
            return restService.SearchRecipes(query, cuisine, intolerances);
        }


        // Shopping list api
        public Task<bool> AddIngredientsToShoppingList(ItemShoppingList iemShoppingList)
        {
            return restService.AddIngredientsToShoppingList(iemShoppingList);
        }

        public Task<ShoppingListResult> GetShoppingList()
        {
            return restService.GetShoppingList();
        }

        public Task<bool> DeleteShoppingListItem(string itemId)
        {
            return restService.DeleteShoppingListItem(itemId);
        }

        public Task<IngredientInform> GetIngredientInform(int id)
        {
            return restService.GetIngredientInform(id);
        }

        public Task<SearchIngredientsResult> SearchIngredients(string searchString)
        {
            return restService.SearchIngredients(searchString);
        }

        // PantryBuilder Api
        public Task<PantryBuilderResult> GetPantrybuilderList()
        {
            return restService.GetPantrybuilderList();
        }

        public Task<PantryBuilderResult> SearchPantryBuilder(string searchString)
        {
            return restService.SearchPantryBuilder(searchString);
        }

        // Pantry api
        public Task<bool> AddItemToUserPantry(UserPantryItem userPantryItem)
        {
            return restService.AddItemToUserPantry(userPantryItem);
        }
    }
}
